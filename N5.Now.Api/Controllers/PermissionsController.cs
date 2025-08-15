using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5.Now.Application.Commands;
using N5.Now.Application.DTOs;
using N5.Now.Application.Queries;

namespace N5.Now.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PermissionsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<PermissionDto>> Create([FromBody] CreatePermissionCommand cmd, CancellationToken ct)
    {
        var result = await _mediator.Send(cmd, ct);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<PermissionDto>> Modify(long id, [FromBody] ModifyPermissionCommand cmd, CancellationToken ct)
    {
        if (id != cmd.Id) return BadRequest("Route id and payload id must match.");
        var result = await _mediator.Send(cmd, ct);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PermissionDto>>> Get([FromQuery] long? id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetPermissionQuery(id), ct);
        return Ok(result);
    }
}
