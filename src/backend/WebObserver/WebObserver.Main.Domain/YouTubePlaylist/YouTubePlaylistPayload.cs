using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.YouTubePlaylist;

public class YouTubePlaylistPayload : ObservingPayload
{
    public List<YouTubePlaylistItem> Items { get; set; } = [];
}