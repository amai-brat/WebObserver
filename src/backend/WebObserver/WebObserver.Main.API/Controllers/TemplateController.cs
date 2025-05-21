using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebObserver.Main.API.Helpers;
using WebObserver.Main.Application.Features.Observings.Queries.GetTemplates;

namespace WebObserver.Main.API.Controllers;

[ApiController]
[Route("templates")]
public class TemplateController(
    IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTemplates(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetTemplatesQuery(), cancellationToken);
        
        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Errors.ToProblemDetails());
    }
}