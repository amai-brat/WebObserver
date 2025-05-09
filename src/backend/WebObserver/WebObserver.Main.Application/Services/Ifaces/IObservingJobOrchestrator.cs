using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Application.Services.Ifaces;

public interface IObservingJobOrchestrator
{
    void AddObservingJob(ObservingBase observingBase);
}