namespace WebObserver.Main.Domain.YouTubePlaylist;

public class YouTubePlaylistItemEqualityComparer : IEqualityComparer<YouTubePlaylistItem>
{
    public bool Equals(YouTubePlaylistItem? x, YouTubePlaylistItem? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null) return false;
        if (y is null) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.VideoId == y.VideoId &&
               x.Title == y.Title && 
               x.Description == y.Description &&
               x.ThumbnailUrl == y.ThumbnailUrl &&
               x.VideoOwnerChannelTitle == y.VideoOwnerChannelTitle &&
               x.Status == y.Status;
    }

    public int GetHashCode(YouTubePlaylistItem obj)
    {
        var hashCode = new HashCode();
        hashCode.Add(obj.VideoId);
        hashCode.Add(obj.Title);
        hashCode.Add(obj.Description);
        hashCode.Add(obj.ThumbnailUrl);
        hashCode.Add(obj.VideoOwnerChannelTitle);
        hashCode.Add(obj.Status);
        return hashCode.ToHashCode();
    }
}