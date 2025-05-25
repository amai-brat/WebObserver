using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.YouTubePlaylist;

public class YouTubePlaylistObserving : ObservingBase
{
    protected YouTubePlaylistObserving()
    {
    }
    
    public YouTubePlaylistObserving(
        ObservingTemplate template, 
        string cronExpression, 
        string playlistId) : base(template, cronExpression)
    {
        PlaylistId = playlistId;
    }

    public string? PlaylistName { get; set; }
    public string PlaylistId { get; init; } = null!;
    public List<UnavailableYouTubePlaylistItem> UnavailableItems { get; set; } = [];
}