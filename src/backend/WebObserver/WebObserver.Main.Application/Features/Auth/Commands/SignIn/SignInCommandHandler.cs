using System.Security.Cryptography;
using System.Text;
using FluentResults;
using WebObserver.Main.Application.Cqrs.Commands;
using WebObserver.Main.Application.Services.Ifaces;
using WebObserver.Main.Domain.Repositories;

namespace WebObserver.Main.Application.Features.Auth.Commands.SignIn;

public class SignInCommandHandler(
    IUserRepository userRepository,
    ITokenService tokenService) : ICommandHandler<SignInCommand, TokenDto>
{
    public async Task<Result<TokenDto>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserAsync(request.Email, cancellationToken);
        if (user is null)
        {
            return Result.Fail("User not found");
        }

        using var hmac = new HMACSHA512(user.PasswordSalt);
        if (!hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password))
            .SequenceEqual(user.PasswordHash))
        {
            return Result.Fail("Invalid password");
        }
        
        var token = tokenService.GenerateToken(user);
        return Result.Ok(new TokenDto
        {
            AccessToken = token
        });
    }
}