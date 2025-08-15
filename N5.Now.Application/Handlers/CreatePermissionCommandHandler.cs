using MediatR;
using N5.Now.Application.Commands;
using N5.Now.Application.DTOs;
using N5.Now.Application.Interfaces;
using N5.Now.Domain.Entities;

namespace N5.Now.Application.Handlers;

public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, PermissionDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IElasticsearchService _es;

    public CreatePermissionCommandHandler(IUnitOfWork uow, IElasticsearchService es)
    {
        _uow = uow;
        _es = es;
    }

    public async Task<PermissionDto> Handle(CreatePermissionCommand request, CancellationToken ct)
    {
        var entity = new Permission
        {
            EmployeeFirstName = request.EmployeeFirstName.Trim(),
            EmployeeLastName = request.EmployeeLastName.Trim(),
            PermissionTypeId = request.PermissionTypeId,
            PermissionDate = request.PermissionDate
        };

        await _uow.Permissions.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        var saved = await _uow.Permissions.GetByIdAsync(entity.Id, ct)
                    ?? entity;

        var dto = new PermissionDto(
            saved.Id,
            saved.EmployeeFirstName,
            saved.EmployeeLastName,
            saved.PermissionTypeId,
            saved.PermissionType?.Description ?? string.Empty,
            saved.PermissionDate
        );

        await _es.IndexAsync("permissions", dto, ct);

        return dto;
    }
}
