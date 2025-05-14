using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.Repositories;

public interface IObservingRepository
{
    Task<ObservingBase?> GetByIdWithUserAsync(int id, CancellationToken cancellationToken = default);
    Task<ObservingEntry<TPayload>?> GetLastEntryByObservingIdAsync<TPayload>(int observingId, CancellationToken cancellationToken = default) where TPayload : ObservingPayload;
}