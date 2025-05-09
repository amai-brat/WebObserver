using FluentResults;
using FluentValidation;
using WebObserver.Main.Application.Cqrs.Commands;
using WebObserver.Main.Application.Features.Errors;
using WebObserver.Main.Application.Features.Observings.Commands.AddObserving.Factories;
using WebObserver.Main.Application.Helpers;
using WebObserver.Main.Application.Services.Ifaces;
using WebObserver.Main.Domain.Repositories;

namespace WebObserver.Main.Application.Features.Observings.Commands.AddObserving;

public class AddObservingCommandHandler(
    IObservingJobOrchestrator observingJobOrchestrator,
    IEnumerable<IValidator<AddObservingCommand>> validators,
    IObservingFactoryResolver factoryResolver,
    IUserRepository userRepository,
    IObservingTemplateRepository templateRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddObservingCommand, AddObservingResponse>
{
    public async Task<Result<AddObservingResponse>> Handle(AddObservingCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validators.ToValidationResult(request);
        if (validationResult.IsFailed)
        {
            return validationResult;
        }
        
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Fail(new UserNotFoundError(request.UserId));
        }
        var template = await templateRepository.GetByIdAsync(request.Request.TemplateId, cancellationToken);
        if (template is null)
        {
            return Result.Fail(new TemplateNotFoundError(request.Request.TemplateId));
        }
        
        var factory = factoryResolver.Resolve(request.Request);
        var observingResult = await factory.CreateAsync(request.Request, template);
        if (observingResult.IsFailed)
        {
            return Result.Fail(observingResult.Errors);
        }

        user.AddObserving(observingResult.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        observingJobOrchestrator.AddObservingJob(observingResult.Value);
        
        return new AddObservingResponse
        {
            ObservingId = observingResult.Value.Id,
        };
    }
}