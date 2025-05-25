using Microsoft.Extensions.DependencyInjection;

namespace WebObserver.Main.Infrastructure.Jobs.YouTubePlaylist;

public class YouTubePlaylistJobService(IServiceScopeFactory scopeFactory) : IJobService
{
    public async Task ObserveAsync(int observingId, CancellationToken cancellationToken = default)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var helper = scope.ServiceProvider.GetRequiredService<YouTubePlaylistJobHelper>();
        await helper.ObserveInternalAsync(observingId, cancellationToken);
    }
}