using FluentResults;
using WebObserver.Main.Application.Cqrs.Commands;
using WebObserver.Main.Application.Features.Errors;
using WebObserver.Main.Application.Services.Ifaces;
using WebObserver.Main.Domain.Repositories;

namespace WebObserver.Main.Application.Features.Observings.Commands.RemoveObserving;

public class RemoveObservingCommandHandler(
    IObservingJobOrchestrator observingJobOrchestrator,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveObservingCommand>
{
    public async Task<Result> Handle(RemoveObservingCommand request, CancellationToken cancellationToken)
    {
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
        
        user.RemoveObserving(observing);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        observingJobOrchestrator.RemoveObservingJob(observing);
        
        return Result.Ok();
    }
}