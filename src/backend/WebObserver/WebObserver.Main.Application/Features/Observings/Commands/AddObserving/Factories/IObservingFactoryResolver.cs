namespace WebObserver.Main.Application.Features.Observings.Commands.AddObserving.Factories;

public interface IObservingFactoryResolver
{
    IObservingFactory Resolve(BaseObservingRequest request);
}