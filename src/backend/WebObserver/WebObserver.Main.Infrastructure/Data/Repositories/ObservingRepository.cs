using Microsoft.EntityFrameworkCore;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Repositories;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Domain.YouTubePlaylist;

namespace WebObserver.Main.Infrastructure.Data.Repositories;

public class ObservingRepository(AppDbContext dbContext) : IObservingRepository
{
    public async Task<ObservingBase?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var observing = await dbContext.Observings
            .Include(x => (x as YouTubePlaylistObserving)!.UnavailableItems)
                .ThenInclude(x => x.CurrentItem)
            .Include(x => (x as YouTubePlaylistObserving)!.UnavailableItems)
                .ThenInclude(x => x.SavedItem)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return observing;
    }

    public async Task<ObservingBase?> GetByIdWithUserAsync(int id, CancellationToken cancellationToken = default)
    {
        var observing = await dbContext.Observings
            .Include(x => x.User)
            .Include(x => (x as YouTubePlaylistObserving)!.UnavailableItems)
                .ThenInclude(x => x.CurrentItem)
            .Include(x => (x as YouTubePlaylistObserving)!.UnavailableItems)
                .ThenInclude(x => x.SavedItem)
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

    public async Task<IEnumerable<ObservingEntryBase>?> GetEntriesAsync(
        int observingId, 
        int page, 
        int pageSize, 
        CancellationToken cancellationToken = default)
    {
        if (page <= 0)
        {
            return [];
        }
        
        var entries = await dbContext.ObservingEntries
            .Where(x => x.ObservingId == observingId)
            .OrderByDescending(x => x.OccuredAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        return entries;
    }

    public async Task<int> GetEntriesCountAsync(int observingId, CancellationToken cancellationToken = default)
    {
        var count = await dbContext.ObservingEntries
            .Where(x => x.ObservingId == observingId)
            .CountAsync(cancellationToken: cancellationToken);
        
        return count;
    }

    public async Task<ObservingEntryBase?> GetEntryAsync(int observingId, int entryId, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.ObservingEntries
            .Include(o => o.Payload)
                .ThenInclude(p => (p as YouTubePlaylistPayload)!.Items)
            .Include(o => o.LastDiff)
            .FirstOrDefaultAsync(x => x.ObservingId == observingId && x.Id == entryId, cancellationToken);
        
        return entry;
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
    
    public async Task<ObservingEntryBase?> GetLastEntryByObservingIdAsync(
        int observingId, 
        CancellationToken cancellationToken = default) 
    {
        var entry = await dbContext.ObservingEntries
            .Where(x => x.ObservingId == observingId)
            .Include(x => x.Payload)
                .ThenInclude(x => (x as YouTubePlaylistPayload)!.Items)
            .OrderByDescending(x => x.OccuredAt)
            .FirstOrDefaultAsync(cancellationToken);
        return entry;
    }
}