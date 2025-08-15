namespace N5.Now.Application.DTOs;

public record PermissionDto(
    long Id,
    string EmployeeFirstName,
    string EmployeeLastName,
    int PermissionTypeId,
    string PermissionTypeDescription,
    DateTime PermissionDate
);
