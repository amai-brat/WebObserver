using Hangfire;
using Hangfire.Common;
using WebObserver.Main.Application.Services.Ifaces;
using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Infrastructure.Jobs;

public class ObservingJobOrchestrator(IRecurringJobManager recurringJobManager) : IObservingJobOrchestrator
{
    public async Task AddObservingJob(ObservingBase observingBase)
    {
        
    }
}