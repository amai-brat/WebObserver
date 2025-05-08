using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.YouTubePlaylist;

public class YouTubePlaylistPayload : ObservingPayload
{
    public List<YouTubePlaylistItem> Items { get; set; } = [];
}

public class YouTubePlaylistItem
{
    public required string VideoId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public long Position { get; set; }
    public string? ThumbnailUrl { get; set; }
    public DateTime PublishedAt { get; set; }
    public required string VideoOwnerChannelTitle { get; set; }
}