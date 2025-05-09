namespace WebObserver.Main.Application.Services.Ifaces;

public interface IYouTubePlaylistService
{
   Task ObserveAsync(int observingId, CancellationToken cancellationToken = default);
}