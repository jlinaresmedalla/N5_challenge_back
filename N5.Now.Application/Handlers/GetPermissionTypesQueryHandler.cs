using MediatR;
using N5.Now.Application.DTOs;
using N5.Now.Application.Interfaces;
using N5.Now.Application.Queries;

namespace N5.Now.Application.Handlers;

public class GetPermissionTypesQueryHandler
    : IRequestHandler<GetPermissionTypesQuery, IEnumerable<PermissionTypeDto>>
{
    private readonly IUnitOfWork _uow;

    public GetPermissionTypesQueryHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<IEnumerable<PermissionTypeDto>> Handle(GetPermissionTypesQuery request, CancellationToken ct)
    {
        var data = await _uow.PermissionTypes.GetAllAsync(ct);
        return data.Select(t => new PermissionTypeDto(t.Id, t.Description));
    }
}
