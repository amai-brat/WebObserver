using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.YouTubePlaylist;

public class YouTubePlaylistDiffPayload : DiffPayload
{
    public required List<YouTubePlaylistItem> Added { get; set; } = [];
    public required List<YouTubePlaylistItem> Removed { get; set; } = [];
    public required List<YouTubePlaylistItem> Changed { get; set; } = [];
    public required List<YouTubePlaylistItem> Unavailable { get; set; } = [];
    public override bool IsEmpty => Added.Count + Removed.Count + Changed.Count + Unavailable.Count == 0;
}