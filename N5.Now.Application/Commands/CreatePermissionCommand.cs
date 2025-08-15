using MediatR;
using N5.Now.Application.DTOs;

namespace N5.Now.Application.Commands;

public record CreatePermissionCommand(
    string EmployeeFirstName,
    string EmployeeLastName,
    int PermissionTypeId,
    DateTime PermissionDate
) : IRequest<PermissionDto>;
