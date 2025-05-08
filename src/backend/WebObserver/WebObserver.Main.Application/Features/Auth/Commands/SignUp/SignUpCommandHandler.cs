using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using FluentResults;
using WebObserver.Main.Application.Cqrs.Commands;
using WebObserver.Main.Domain.Entities;
using WebObserver.Main.Domain.Repositories;

namespace WebObserver.Main.Application.Features.Auth.Commands.SignUp;

public class SignUpCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<SignUpCommand>
{
    public async Task<Result> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var validationResult = Validate(request);
        if (validationResult.IsFailed)
        {
            return validationResult;
        }
        
        var exist = await userRepository.IsUserExistAsync(request.Email, cancellationToken);
        if (exist)
        {
            return Result.Fail($"Email {request.Email} already exists");
        }

        using var hmac = new HMACSHA512();
        var salt = hmac.Key;
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        
        var user = new User(request.Name, request.Email, hash, salt);
        _ = await userRepository.AddAsync(user, cancellationToken);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }

    private static Result Validate(SignUpCommand request)
    {
        try
        {
            _ = new MailAddress(request.Email);
        }
        catch (Exception)
        {
            return Result.Fail("Invalid email address");
        }
        
        return Result.Ok();
    }
}