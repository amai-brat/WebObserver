using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.YouTubePlaylist;

public class YouTubePlaylistPayload : ObservingPayload
{
    public List<YouTubePlaylistItem> Items { get; set; } = [];
    public override ObservingPayloadSummary CreateSummary()
    {
        return new YouTubePlaylistPayloadSummary
        {
            ItemsCount = Items.Count,
            UnavailableItemsCount = Items.Count(x => !x.IsAvailable),
        };
    }
}

public class YouTubePlaylistPayloadSummary : ObservingPayloadSummary
{
    public int ItemsCount { get; set; }
    public int UnavailableItemsCount { get; set; }
}