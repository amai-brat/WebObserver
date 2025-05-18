using FluentResults;
using FluentValidation;
using WebObserver.Main.Application.Cqrs.Commands;
using WebObserver.Main.Application.Features.Errors;
using WebObserver.Main.Application.Helpers;
using WebObserver.Main.Application.Services.Ifaces;
using WebObserver.Main.Domain.Repositories;

namespace WebObserver.Main.Application.Features.Observings.Commands.EditObserving;

public class EditObservingCommandHandler(
    IObservingJobOrchestrator observingJobOrchestrator,
    IEnumerable<IValidator<EditObservingCommand>> validators,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<EditObservingCommand>
{
    public async Task<Result> Handle(EditObservingCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validators.ToValidationResult(request);
        if (validationResult.IsFailed)
        {
            return validationResult;
        }
        
        var user = await userRepository.GetByIdWithObservingsAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Fail(new UserNotFoundError(request.UserId));
        }
        
        var observing = user.Observings.SingleOrDefault(o => o.Id == request.ObservingId);
        if (observing is null)
        {
            return Result.Fail(new ObservingNotFoundError());
        }
        
        observing.CronExpression = request.Request.CronExpression ?? observing.CronExpression;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        observingJobOrchestrator.EditObservingJob(observing);
        
        return Result.Ok();
    }
}