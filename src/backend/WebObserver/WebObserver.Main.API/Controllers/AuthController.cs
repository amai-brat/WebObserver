using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebObserver.Main.API.Helpers;
using WebObserver.Main.Application.Features.Auth.Commands.SignIn;
using WebObserver.Main.Application.Features.Auth.Commands.SignUp;

namespace WebObserver.Main.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        
        return result.IsSuccess 
            ? Ok() 
            : BadRequest(result.Errors.ToProblemDetails());
    }
    
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        
        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Errors.ToProblemDetails());
    }
    
    
}