namespace WebObserver.Main.Application.Features.Observings.Commands.AddObserving.Factories;

public sealed class ObservingFactoryResolver(IEnumerable<IObservingFactory> factories) : IObservingFactoryResolver
{
    private readonly Dictionary<Type, IObservingFactory> _factories = factories
        .ToDictionary(f => f.RequestType, f => f);

    public IObservingFactory Resolve(BaseObservingRequest request)
    {
        if (_factories.TryGetValue(request.GetType(), out var factory))
        {
            return factory;
        }

        throw new ArgumentException($"Factory for type ${request.GetType().Name} doesn't exist", nameof(request));
    }
}