using MediatR;
using N5.Now.Application.DTOs;

namespace N5.Now.Application.Queries;

public record GetPermissionTypesQuery() : IRequest<IEnumerable<PermissionTypeDto>>;
