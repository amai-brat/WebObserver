using FluentResults;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebObserver.Main.Domain.Repositories;
using WebObserver.Main.Domain.YouTubePlaylist;
using WebObserver.Main.Infrastructure.Mappers;

namespace WebObserver.Main.Infrastructure.Jobs.YouTubePlaylist;

public class YouTubePlaylistJobService(IServiceScopeFactory scopeFactory) : IJobService
{
    public async Task ObserveAsync(int observingId, CancellationToken cancellationToken = default)
    {
        await using (var scope = scopeFactory.CreateAsyncScope())
        {
            var observingRepo = scope.ServiceProvider.GetRequiredService<IObservingRepository>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<YouTubePlaylistJobService>>();
            var ytService = scope.ServiceProvider.GetRequiredService<YouTubeService>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            
            var observing = await observingRepo.GetByIdWithUserAsync(observingId, cancellationToken);
            if (observing is not YouTubePlaylistObserving ytObserving)
            {
                throw new InvalidOperationException($"{nameof(YouTubePlaylistJobService)} is used for {observing?.GetType().Name ?? "null"}");
            }
            
            var entryResult = await CreateEntryAsync(ytService, ytObserving, cancellationToken);
            if (entryResult.IsFailed)
            {
                logger.LogWarning("Couldn't create YouTube playlist entry at {Time} with {Error}", DateTime.UtcNow, string.Join("\n", entryResult.Errors));
                return;
            }
            
            var prevEntry = await observingRepo.GetLastEntryByObservingIdAsync(ytObserving.Id, cancellationToken);
            ytObserving.AddEntry(entryResult.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        
    }

    private static async Task<Result<YouTubePlaylistObservingEntry>> CreateEntryAsync(
        YouTubeService youTubeService, 
        YouTubePlaylistObserving ytObserving, 
        CancellationToken cancellationToken = default)
    {
        var itemsResult = await RetrievePlaylistItemsAsync(youTubeService, ytObserving.PlaylistId, cancellationToken);
        if (itemsResult.IsFailed)
        {
            return itemsResult.ToResult<YouTubePlaylistObservingEntry>();
        }

        var payload = new YouTubePlaylistPayload
        {
            Items = itemsResult.Value
        };
        
        var entry = new YouTubePlaylistObservingEntry
        {
            ObservingId = ytObserving.Id,
            OccuredAt = DateTime.UtcNow,
            Payload = payload,
            PayloadSummary = payload.CreateSummary(),
            DiffSummary = null
        };

        return Result.Ok(entry);
    }

    private static async Task<Result<List<YouTubePlaylistItem>>> RetrievePlaylistItemsAsync(
        YouTubeService youTubeService,
        string playlistId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            List<YouTubePlaylistItem> result = [];

            var req = youTubeService.PlaylistItems.List("snippet, status");
            req.PlaylistId = playlistId;
            req.MaxResults = 50;
            var response = await req.ExecuteAsync(cancellationToken);

            while (true)
            {
                result.AddRange(response.Items.Select(item => item.ToYouTubePlaylistItem()));

                if (response.NextPageToken is not null)
                {
                    req.PageToken = response.NextPageToken;
                    response = await req.ExecuteAsync(cancellationToken);
                }
                else
                {
                    break;
                }
            }
            
            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error fetching playlist items: {ex.Message}");
        }
    }
}