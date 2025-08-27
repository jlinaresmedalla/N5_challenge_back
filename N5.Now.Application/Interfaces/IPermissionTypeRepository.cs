using N5.Now.Domain.Entities;

namespace N5.Now.Application.Interfaces;

public interface IPermissionTypeRepository
{
    Task<IReadOnlyList<PermissionType>> GetAllAsync(CancellationToken ct);
}
