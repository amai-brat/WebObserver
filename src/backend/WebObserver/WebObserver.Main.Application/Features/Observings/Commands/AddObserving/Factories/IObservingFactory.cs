using FluentResults;
using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Application.Features.Observings.Commands.AddObserving.Factories;

public interface IObservingFactory
{
    Type RequestType { get; }
    Task<Result<ObservingBase>> CreateAsync(BaseObservingRequest request, ObservingTemplate template);
}