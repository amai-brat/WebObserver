namespace WebObserver.Main.Domain.YouTubePlaylist;

public class YouTubePlaylistItem
{
    private static readonly List<string> UnavailableStatuses = ["private", "privacyStatusUnspecified"];
    
    public int Id { get; set; }
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    
    public required string VideoId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public long Position { get; set; }
    public string? ThumbnailUrl { get; set; }
    public DateTime PublishedAt { get; set; }
    public required string? VideoOwnerChannelTitle { get; set; }
    public required string Status { get; set; }

    public bool IsAvailable => !UnavailableStatuses.Contains(Status);
    

    public bool IsChanged(YouTubePlaylistItem otherState)
    {
        return Status != otherState.Status;
    }
}

public class UnavailableYouTubePlaylistItem
{
    public int Id { get; set; }
    
    public int? SavedItemId { get; set; }
    public YouTubePlaylistItem? SavedItem { get; set; }

    public int CurrentItemId { get; set; }
    public required YouTubePlaylistItem CurrentItem { get; set; }
}