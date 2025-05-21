using Microsoft.Extensions.DependencyInjection;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.YouTubePlaylist;

namespace WebObserver.Main.Infrastructure.Jobs.YouTubePlaylist;

public class YouTubePlaylistJobServiceFactory(IServiceScopeFactory  scopeFactory) : IJobServiceFactory
{
    public Type ObservingType => typeof(YouTubePlaylistObserving);
    
    public string GenerateJobId(ObservingBase observing)
    {
        return $"{observing.UserId}:yt-playlist:{observing.Id}";
    }

    public IJobService CreateService()
    {
        return new YouTubePlaylistJobService(scopeFactory);
    }
}