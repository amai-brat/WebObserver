using Microsoft.EntityFrameworkCore;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Repositories;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Domain.YouTubePlaylist;

namespace WebObserver.Main.Infrastructure.Data.Repositories;

public class ObservingRepository(AppDbContext dbContext) : IObservingRepository
{
    public async Task<ObservingBase?> GetByIdWithUserAsync(int id, CancellationToken cancellationToken = default)
    {
        var observing = await dbContext.Observings
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return observing;
    }

    public async Task<ObservingBase?> GetByIdWithEntriesSummaryAsync(int id, CancellationToken cancellationToken = default)
    {
        var observing = await dbContext.Observings
            .Include(x => x.Template)
            .Include(x => (x as TextObserving)!.Entries)
                .ThenInclude(x => x.LastDiff)
            .Include(x => (x as YouTubePlaylistObserving)!.Entries)
                .ThenInclude(x => x.LastDiff)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        
        return observing;
    }

    public async Task<IEnumerable<ObservingEntryBase>?> GetEntriesAsync(int observingId, CancellationToken cancellationToken = default)
    {
        var entries = await dbContext.ObservingEntries
            .Where(x => x.ObservingId == observingId)
            .ToListAsync(cancellationToken: cancellationToken);

        return entries;
    }

    public async Task<ObservingBase?> GetByIdWithEntriesAsync(int id, CancellationToken cancellationToken = default)
    {
        var observing = await dbContext.Observings
            .Include(x => x.Template)
            .Include(x => (x as TextObserving)!.Entries)
                .ThenInclude(x => x.Payload)
            .Include(x => (x as TextObserving)!.Entries)
                .ThenInclude(x => x.LastDiff)
            .Include(x => (x as YouTubePlaylistObserving)!.Entries)
                .ThenInclude(x => x.Payload)
            .Include(x => (x as YouTubePlaylistObserving)!.Entries)
                .ThenInclude(x => x.LastDiff)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        
        return observing;
    }
    
    public async Task<ObservingEntry<TPayload, TDiffPayload>?> GetLastEntryByObservingIdAsync<TPayload, TDiffPayload>(
        int observingId, 
        CancellationToken cancellationToken = default) 
        where TPayload : ObservingPayload
        where TDiffPayload : DiffPayload
    {
        var entry = await dbContext.ObservingEntries
            .Where(x => x.ObservingId == observingId)
            .OfType<ObservingEntry<TPayload, TDiffPayload>>()
            .Include(x => x.Payload)
            .OrderByDescending(x => x.OccuredAt)
            .FirstOrDefaultAsync(cancellationToken);
        return entry;
    }
}