using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest;
using N5.Now.Application.Interfaces;

namespace N5.Now.Infrastructure.Elasticsearch;

public class ElasticsearchSettings
{
    public string Uri { get; set; } = "http://localhost:9200";
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string IndexName { get; set; } = "permissions";
}

public class ElasticsearchService : IElasticsearchService
{
    private readonly ElasticClient _client;
    private readonly ILogger<ElasticsearchService> _logger;
    private readonly string _index;

    public ElasticsearchService(IOptions<ElasticsearchSettings> options, ILogger<ElasticsearchService> logger)
    {
        _logger = logger;
        var cfg = options.Value;
        _index = string.IsNullOrWhiteSpace(cfg.IndexName) ? "permissions" : cfg.IndexName;

        var settings = new ConnectionSettings(new Uri(cfg.Uri))
            .ThrowExceptions(false);

        if (!string.IsNullOrWhiteSpace(cfg.Username))
            settings = settings.BasicAuthentication(cfg.Username!, cfg.Password ?? "");

        _client = new ElasticClient(settings);
    }

    public async Task IndexAsync<T>(string indexName, T document, CancellationToken ct) where T : class
    {
        var idx = string.IsNullOrWhiteSpace(indexName) ? _index : indexName;

        await EnsureIndexAsync(idx, ct);

        var resp = await _client.IndexAsync(document, i => i.Index(idx), ct);

        if (!resp.IsValid)
        {
            _logger.LogError("ES index error. Index={Index} Reason={Reason}",
                idx,
                resp.OriginalException?.Message ?? resp.ServerError?.ToString() ?? "unknown");

            return;
        }
    }

    private async Task EnsureIndexAsync(string indexName, CancellationToken ct)
    {
        try
        {
            var exists = await _client.Indices.ExistsAsync(indexName);
            if (exists.Exists) return;

            var create = await _client.Indices.CreateAsync(indexName, c => c
                .Map(m => m.AutoMap()), ct);

            if (!create.IsValid)
            {
                _logger.LogWarning("No se pudo crear el índice ES '{Index}'. Reason={Reason}",
                    indexName,
                    create.OriginalException?.Message ?? create.ServerError?.ToString() ?? "unknown");
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Fallo comprobando/creando índice '{Index}' en ES", indexName);
        }
    }
}
