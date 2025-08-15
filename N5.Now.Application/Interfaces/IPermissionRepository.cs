using N5.Now.Domain.Entities;

namespace N5.Now.Application.Interfaces;

public interface IPermissionRepository
{
    Task<Permission?> GetByIdAsync(long id, CancellationToken ct);
    Task<IReadOnlyList<Permission>> GetAllAsync(CancellationToken ct);
    Task AddAsync(Permission entity, CancellationToken ct);
    void Update(Permission entity);
}
