using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.Repositories;

public interface IObservingRepository
{
    Task<ObservingBase?> GetByIdWithUserAsync(int id, CancellationToken cancellationToken = default);
    Task<ObservingBase?> GetByIdWithEntriesSummaryAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ObservingEntryBase>?> GetEntriesAsync(
        int observingId, 
        CancellationToken cancellationToken = default);
    Task<ObservingPayload?> GetEntryPayloadAsync(
        int observingId, 
        int entryId,
        CancellationToken cancellationToken = default);

    Task<ObservingEntryBase?> GetLastEntryByObservingIdAsync(
        int observingId,
        CancellationToken cancellationToken = default);
}