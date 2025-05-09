using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Infrastructure.Jobs;

public interface IJobServiceFactoryResolver
{ 
    IJobServiceFactory Resolve(ObservingBase observing);
}