using Microsoft.EntityFrameworkCore;
using WebObserver.Main.Domain.Repositories;
using WebObserver.Main.Domain.YouTubePlaylist;

namespace WebObserver.Main.Infrastructure.Data.Repositories;

public class YouTubePlaylistRepository(
    AppDbContext dbContext) : IYouTubePlaylistRepository
{
    public async Task<List<YouTubePlaylistItem>> GetItems(
        IEnumerable<string> videoIds, 
        CancellationToken ct = default)
    {
        var items = await dbContext.YouTubePlaylistItems
            .Where(i => videoIds.Contains(i.VideoId))
            .ToListAsync(cancellationToken: ct);
        
        return items;
    }
}