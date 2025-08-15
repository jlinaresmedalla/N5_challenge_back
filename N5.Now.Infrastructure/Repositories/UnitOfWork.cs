using N5.Now.Application.Interfaces;
using N5.Now.Infrastructure.Persistence;

namespace N5.Now.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public IPermissionRepository Permissions { get; }

    public UnitOfWork(ApplicationDbContext db, IPermissionRepository permissions)
    {
        _db = db;
        Permissions = permissions;
    }

    public Task<int> SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}
