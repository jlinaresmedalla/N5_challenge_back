using Microsoft.EntityFrameworkCore;
using N5.Now.Application.Interfaces;
using N5.Now.Domain.Entities;
using N5.Now.Infrastructure.Persistence;

namespace N5.Now.Infrastructure.Repositories;

public class PermissionTypeRepository : IPermissionTypeRepository
{
    private readonly ApplicationDbContext _db;
    public PermissionTypeRepository(ApplicationDbContext db) => _db = db;

    public async Task<IReadOnlyList<PermissionType>> GetAllAsync(CancellationToken ct)
        => await _db.PermissionTypes.AsNoTracking().OrderBy(x => x.Id).ToListAsync(ct);
}
