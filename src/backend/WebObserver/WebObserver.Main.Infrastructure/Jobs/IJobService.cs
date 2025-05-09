namespace WebObserver.Main.Infrastructure.Jobs;

public interface IJobService
{
    Task ObserveAsync(int observingId, CancellationToken cancellationToken = default);
}