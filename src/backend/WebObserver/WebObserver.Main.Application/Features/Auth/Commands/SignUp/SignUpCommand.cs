using WebObserver.Main.Application.Cqrs.Commands;

namespace WebObserver.Main.Application.Features.Auth.Commands.SignUp;

public record SignUpCommand(string Name, string Email, string Password) : ICommand;