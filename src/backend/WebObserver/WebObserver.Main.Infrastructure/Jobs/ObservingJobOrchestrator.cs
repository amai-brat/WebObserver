using Hangfire;
using WebObserver.Main.Application.Services.Ifaces;
using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Infrastructure.Jobs;

public class ObservingJobOrchestrator(
    IRecurringJobManager recurringJobManager,
    IJobServiceFactoryResolver resolver) : IObservingJobOrchestrator
{
    public void AddObservingJob(ObservingBase observingBase)
    {
        var factory = resolver.Resolve(observingBase);

        var service = factory.CreateService();
        var jobId = factory.GenerateJobId(observingBase);
        
        recurringJobManager.AddOrUpdate(jobId, 
            () => service.ObserveAsync(observingBase.Id, CancellationToken.None), 
            observingBase.CronExpression);
    }

    public void RemoveObservingJob(ObservingBase observingBase)
    {
        var factory = resolver.Resolve(observingBase);
        var jobId = factory.GenerateJobId(observingBase);
        
        recurringJobManager.RemoveIfExists(jobId);
    }
}