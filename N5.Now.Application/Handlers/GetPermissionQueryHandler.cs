using MediatR;
using N5.Now.Application.DTOs;
using N5.Now.Application.Interfaces;
using N5.Now.Application.Queries;

namespace N5.Now.Application.Handlers;

public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, IEnumerable<PermissionDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IElasticsearchService _es;

    public GetPermissionQueryHandler(IUnitOfWork uow, IElasticsearchService es)
    {
        _uow = uow;
        _es = es;
    }

    public async Task<IEnumerable<PermissionDto>> Handle(GetPermissionQuery request, CancellationToken ct)
    {
        var data = await _uow.Permissions.GetAllAsync(ct);

        var filtered = request.Id.HasValue
            ? data.Where(p => p.Id == request.Id.Value)
            : data;

        var list = filtered.Select(p => new PermissionDto(
            p.Id,
            p.EmployeeFirstName,
            p.EmployeeLastName,
            p.PermissionTypeId,
            p.PermissionType?.Description ?? string.Empty,
            p.PermissionDate
        )).ToList();

        var tasks = list.Select(dto => _es.IndexAsync("permissions", dto, ct));
        await Task.WhenAll(tasks);

        return list;
    }
}
