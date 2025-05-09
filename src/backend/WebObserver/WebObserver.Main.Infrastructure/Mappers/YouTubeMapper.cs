using Google.Apis.YouTube.v3.Data;
using WebObserver.Main.Domain.YouTubePlaylist;

namespace WebObserver.Main.Infrastructure.Mappers;

public static class YouTubeMapper
{
    public static YouTubePlaylistItem ToYouTubePlaylistItem(this PlaylistItem item)
    {
        return new YouTubePlaylistItem
        {
            VideoId = item.Snippet.ResourceId.VideoId,
            Title = item.Snippet.Title,
            Description = item.Snippet.Description,
            Position = item.Snippet.Position ?? 0,
            ThumbnailUrl = item.Snippet.Thumbnails?.Default__?.Url,
            PublishedAt = item.Snippet.PublishedAtDateTimeOffset?.UtcDateTime ?? DateTime.UnixEpoch,
            VideoOwnerChannelTitle = item.Snippet.VideoOwnerChannelTitle,
            Status = item.Status.PrivacyStatus
        };
    }
}