using WebObserver.Main.Application.Cqrs.Commands;

namespace WebObserver.Main.Application.Features.Auth.Commands.SignIn;

public record SignInCommand(string Email, string Password) : ICommand<TokenDto>;