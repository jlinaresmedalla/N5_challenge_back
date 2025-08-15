using Microsoft.EntityFrameworkCore;
using N5.Now.Application.Interfaces;
using N5.Now.Domain.Entities;
using N5.Now.Infrastructure.Persistence;

namespace N5.Now.Infrastructure.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly ApplicationDbContext _db;

    public PermissionRepository(ApplicationDbContext db) => _db = db;

    public async Task AddAsync(Permission entity, CancellationToken ct)
        => await _db.Permissions.AddAsync(entity, ct);

    public async Task<IReadOnlyList<Permission>> GetAllAsync(CancellationToken ct)
        => await _db.Permissions
            .Include(p => p.PermissionType)
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task<Permission?> GetByIdAsync(long id, CancellationToken ct)
        => await _db.Permissions
            .Include(p => p.PermissionType)
            .FirstOrDefaultAsync(p => p.Id == id, ct);

    public void Update(Permission entity) => _db.Permissions.Update(entity);
}
