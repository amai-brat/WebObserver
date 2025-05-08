using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebObserver.Main.API.Helpers;
using WebObserver.Main.Application.Features.Observings.Commands;
using WebObserver.Main.Application.Features.Observings.Commands.AddObserving;

namespace WebObserver.Main.API.Controllers;

[Authorize]
[ApiController]
[Route("observings")]
public class ObservingController(
    IMediator mediator) : ControllerBase
{
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
            ? Ok(result.Value) 
            : BadRequest(result.Errors.ToProblemDetails());
    }
}