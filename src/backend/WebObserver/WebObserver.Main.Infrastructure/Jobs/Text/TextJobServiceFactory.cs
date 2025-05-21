using Microsoft.Extensions.DependencyInjection;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Text;

namespace WebObserver.Main.Infrastructure.Jobs.Text;

public class TextJobServiceFactory(IServiceScopeFactory scopeFactory) : IJobServiceFactory
{
    public Type ObservingType => typeof(TextObserving);
    
    public string GenerateJobId(ObservingBase observing)
    {
        return $"{observing.UserId}:text:{observing.Id}";
    }

    public IJobService CreateService()
    {
        return new TextJobService(scopeFactory);
    }
}