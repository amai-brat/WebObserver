using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.Repositories;

public interface IObservingRepository
{
    Task<ObservingBase?> GetByIdWithUserAsync(int id, CancellationToken cancellationToken = default);
    Task<ObservingBase?> GetByIdWithEntriesAsync(int id, CancellationToken cancellationToken = default);
    Task<ObservingEntry<TPayload, TDiffPayload>?> GetLastEntryByObservingIdAsync<TPayload, TDiffPayload>(
        int observingId,
        CancellationToken cancellationToken = default) 
        where TPayload : ObservingPayload 
        where TDiffPayload : DiffPayload;
}