using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.Repositories;

public interface IObservingRepository
{
    Task<ObservingBase?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ObservingBase?> GetByIdWithUserAsync(int id, CancellationToken cancellationToken = default);
    Task<ObservingBase?> GetByIdWithEntriesSummaryAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ObservingEntryBase>?> GetEntriesAsync(
        int observingId, 
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
    Task<int> GetEntriesCountAsync(
        int observingId, 
        CancellationToken cancellationToken = default);
    Task<ObservingEntryBase?> GetEntryAsync(
        int observingId, 
        int entryId,
        CancellationToken cancellationToken = default);

    Task<ObservingEntryBase?> GetLastEntryByObservingIdAsync(
        int observingId,
        CancellationToken cancellationToken = default);
}