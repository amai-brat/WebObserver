using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Infrastructure.Jobs;

public interface IJobServiceFactory
{
    Type ObservingType { get; }
    string GenerateJobId(ObservingBase observing);
    IJobService CreateService();
}