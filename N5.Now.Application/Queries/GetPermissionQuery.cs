using MediatR;
using N5.Now.Application.DTOs;

namespace N5.Now.Application.Queries;

public record GetPermissionQuery(long? Id) : IRequest<IEnumerable<PermissionDto>>;
