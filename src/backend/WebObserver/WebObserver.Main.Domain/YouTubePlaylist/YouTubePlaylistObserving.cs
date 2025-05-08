using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.YouTubePlaylist;

public class YouTubePlaylistObserving : Observing<YouTubePlaylistPayload>
{
    protected YouTubePlaylistObserving() { }
    
    public YouTubePlaylistObserving(ObservingTemplate template, string cronExpression, string playlistId) : base(template, cronExpression)
    {
        PlaylistId = playlistId;
    }

    public required string PlaylistId { get; init; }
}