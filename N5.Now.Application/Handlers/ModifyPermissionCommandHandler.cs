using MediatR;
using N5.Now.Application.Commands;
using N5.Now.Application.DTOs;
using N5.Now.Application.Interfaces;

namespace N5.Now.Application.Handlers;

public class ModifyPermissionCommandHandler : IRequestHandler<ModifyPermissionCommand, PermissionDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IElasticsearchService _es;

    public ModifyPermissionCommandHandler(IUnitOfWork uow, IElasticsearchService es)
    {
        _uow = uow;
        _es = es;
    }

    public async Task<PermissionDto> Handle(ModifyPermissionCommand request, CancellationToken ct)
    {
        var entity = await _uow.Permissions.GetByIdAsync(request.Id, ct)
                     ?? throw new KeyNotFoundException($"Permission {request.Id} not found");

        entity.EmployeeFirstName = request.EmployeeFirstName.Trim();
        entity.EmployeeLastName = request.EmployeeLastName.Trim();
        entity.PermissionTypeId = request.PermissionTypeId;
        entity.PermissionDate = request.PermissionDate;

        _uow.Permissions.Update(entity);
        await _uow.SaveChangesAsync(ct);

        var updated = await _uow.Permissions.GetByIdAsync(entity.Id, ct) ?? entity;

        var dto = new PermissionDto(
            updated.Id,
            updated.EmployeeFirstName,
            updated.EmployeeLastName,
            updated.PermissionTypeId,
            updated.PermissionType?.Description ?? string.Empty,
            updated.PermissionDate
        );

        await _es.IndexAsync("permissions", dto, ct);

        return dto;
    }
}
