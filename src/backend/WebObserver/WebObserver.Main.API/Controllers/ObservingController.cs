using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebObserver.Main.API.Helpers;
using WebObserver.Main.Application.Features.Observings.Commands.AddObserving;
using WebObserver.Main.Application.Features.Observings.Commands.EditObserving;
using WebObserver.Main.Application.Features.Observings.Commands.RemoveObserving;
using WebObserver.Main.Application.Features.Observings.Queries.GetAllObservings;
using WebObserver.Main.Application.Features.Observings.Queries.GetObserving;
using WebObserver.Main.Application.Features.Observings.Queries.GetObservingEntries;

namespace WebObserver.Main.API.Controllers;

[Authorize]
[ApiController]
[Route("observings")]
public class ObservingController(
    IMediator mediator) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetObserving([FromRoute] int id, CancellationToken cancellationToken)
    {
        var userId = this.GetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var result = await mediator.Send(new GetObservingQuery(userId.Value, id), cancellationToken);
        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Errors.ToProblemDetails());
    }
    
    [HttpGet("{id:int}/entries")]
    public async Task<IActionResult> GetObservingEntries([FromRoute] int id, CancellationToken cancellationToken)
    {
        var userId = this.GetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var result = await mediator.Send(new GetObservingEntriesQuery(userId.Value, id), cancellationToken);
        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Errors.ToProblemDetails());
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetObservings(CancellationToken cancellationToken)
    {
        var userId = this.GetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }

        var result = await mediator.Send(new GetAllObservingsQuery(userId.Value), cancellationToken);
        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Errors.ToProblemDetails());
    }
    
    [HttpPost]
    public async Task<IActionResult> AddObserving([FromBody] BaseObservingRequest request)
    {
        var userId = this.GetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }
        
        var result = await mediator.Send(new AddObservingCommand(userId.Value, request));
        return result.IsSuccess 
            ? Created($"/observings/{result.Value.ObservingId}", result.Value) 
            : BadRequest(result.Errors.ToProblemDetails());
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> EditObserving([FromRoute] int id, [FromBody] EditObservingRequest request, CancellationToken cancellationToken)
    {
        var userId = this.GetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }
        
        var result = await mediator.Send(new EditObservingCommand(userId.Value, id, request), cancellationToken);
        return result.IsSuccess 
            ? Ok() 
            : BadRequest(result.Errors.ToProblemDetails());
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveObserving(int id, CancellationToken cancellationToken)
    {
        var userId = this.GetUserId();
        if (userId is null)
        {
            return Unauthorized();
        }
        
        var result = await mediator.Send(new RemoveObservingCommand(userId.Value, id), cancellationToken);
        return result.IsSuccess 
            ? Ok() 
            : BadRequest(result.Errors.ToProblemDetails());
    }
}