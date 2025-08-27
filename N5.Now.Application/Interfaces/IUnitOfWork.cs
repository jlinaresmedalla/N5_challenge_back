namespace N5.Now.Application.Interfaces;

public interface IUnitOfWork
{
    IPermissionRepository Permissions { get; }
    IPermissionTypeRepository PermissionTypes { get; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}
