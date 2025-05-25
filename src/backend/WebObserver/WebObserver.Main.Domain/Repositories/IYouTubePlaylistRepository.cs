using WebObserver.Main.Domain.YouTubePlaylist;

namespace WebObserver.Main.Domain.Repositories;

public interface IYouTubePlaylistRepository
{
    Task<List<YouTubePlaylistItem>> GetItems(IEnumerable<string> videoIds, CancellationToken ct = default);
}