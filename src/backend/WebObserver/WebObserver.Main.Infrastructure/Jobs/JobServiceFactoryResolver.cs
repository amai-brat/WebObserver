using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Infrastructure.Jobs;

public class JobServiceFactoryResolver(IEnumerable<IJobServiceFactory> factories) : IJobServiceFactoryResolver
{
    private readonly Dictionary<Type, IJobServiceFactory> _factories = factories
        .ToDictionary(f => f.ObservingType, f => f);

    public IJobServiceFactory Resolve(ObservingBase observing)
    {
        if (_factories.TryGetValue(observing.GetType(), out var factory))
        {
            return factory;
        }

        throw new ArgumentException($"Factory for type ${observing.GetType().Name} doesn't exist", nameof(observing));
    }
}