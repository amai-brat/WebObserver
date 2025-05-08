using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Domain.YouTubePlaylist;

public class YouTubePlaylistObservingTemplate : ObservingTemplate
{
    public override ObservingType Type => ObservingType.YoutubePlaylist;
}