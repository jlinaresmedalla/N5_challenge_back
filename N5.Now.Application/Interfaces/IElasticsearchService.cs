namespace N5.Now.Application.Interfaces;

public interface IElasticsearchService
{
    Task IndexAsync<T>(string indexName, T document, CancellationToken ct) where T : class;
}
