using Microsoft.EntityFrameworkCore;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Repositories;

namespace WebObserver.Main.Infrastructure.Data.Repositories;

public class ObservingRepository(AppDbContext dbContext) : IObservingRepository
{
    public async Task<ObservingBase?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var observing = await dbContext.Observings.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return observing;
    }

    public async Task<ObservingEntry<TPayload>?> GetLastEntryByObservingIdAsync<TPayload>(
        int observingId, 
        CancellationToken cancellationToken = default) 
        where TPayload : ObservingPayload
    {
        var entry = await dbContext.ObservingEntries
            .Where(x => x.ObservingId == observingId)
            .OfType<ObservingEntry<TPayload>>()
            .Include(x => x.Payload)
            .OrderByDescending(x => x.OccuredAt)
            .FirstOrDefaultAsync(cancellationToken);
        return entry;
    }
}