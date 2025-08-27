using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5.Now.Application.DTOs;
using N5.Now.Application.Queries;

namespace N5.Now.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionTypesController : ControllerBase
{
    private readonly IMediator _mediator;
    public PermissionTypesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PermissionTypeDto>>> GetAll(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetPermissionTypesQuery(), ct);
        return Ok(result);
    }
}
