using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.YouTubePlaylist;

public class YouTubePlaylistDiffPayload : DiffPayload
{
    public required List<YouTubePlaylistItem> Added { get; set; } = [];
    public required List<YouTubePlaylistItem> Removed { get; set; } = [];
    public required List<BeforeAfter<YouTubePlaylistItem>> Changed { get; set; } = [];
    public required List<BeforeAfter<YouTubePlaylistItem>> Unavailable { get; set; } = [];
    public override bool IsEmpty => Added.Count + Removed.Count + Changed.Count + Unavailable.Count == 0;
    public override DiffSummary CreateSummary()
    {
        return new YouTubePlaylistDiffSummary
        {
            Added = Added.Count,
            Removed = Removed.Count,
            Changed = Changed.Count,
            Unavailable = Unavailable.Count,
        };
    }
}

public class YouTubePlaylistDiffSummary : DiffSummary
{
    public int Added { get; set; }
    public int Removed { get; set; }
    public int Changed { get; set; }
    public int Unavailable { get; set; }
}