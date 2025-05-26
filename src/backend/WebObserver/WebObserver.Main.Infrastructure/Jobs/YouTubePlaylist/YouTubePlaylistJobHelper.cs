using System.Diagnostics.CodeAnalysis;
using FluentResults;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Repositories;
using WebObserver.Main.Domain.Services;
using WebObserver.Main.Domain.YouTubePlaylist;
using WebObserver.Main.Infrastructure.Mappers;

namespace WebObserver.Main.Infrastructure.Jobs.YouTubePlaylist;

public class YouTubePlaylistJobHelper(
    [FromKeyedServices(nameof(ObservingType.YouTubePlaylist))] IDiffGenerator diffGenerator,
    IObservingRepository observingRepo,
    YouTubeService youtubeService,
    IYouTubePlaylistRepository youtubePlaylistRepo,
    ILogger<YouTubePlaylistJobHelper> logger,
    INotifier notifier,
    IUnitOfWork unitOfWork)
    : ObservingJobHelperBase<YouTubePlaylistObserving, YouTubePlaylistObservingEntry, YouTubePlaylistPayload>(
        observingRepo,
        logger,
        notifier,
        diffGenerator,
        unitOfWork)
{
    protected override async Task<Result<YouTubePlaylistPayload>> FetchPayloadAsync(
        YouTubePlaylistObserving observing, 
        CancellationToken ct)
    {
        var itemsResult = await GetPlaylistItemsAsync(observing.PlaylistId, ct);
        if (itemsResult.IsFailed)
        {
            return itemsResult.ToResult<YouTubePlaylistPayload>();
        }

        var comparer = new YouTubePlaylistItemEqualityComparer();
        
        var items = itemsResult.Value;
        var savedItems = await youtubePlaylistRepo
            .GetItems(itemsResult.Value.Select(x => x.VideoId), ct);
        var result = new List<YouTubePlaylistItem>();
        
        foreach (var item in items)
        {
            result.Add(TryGetSavedItem(item.VideoId, savedItems, skipUnavailable: false, out var savedItem) &&
                       comparer.Equals(item, savedItem)
                ? savedItem 
                : item);
        }
        
        return new YouTubePlaylistPayload
        {
            Items = result
        };
    }

    protected override YouTubePlaylistObservingEntry CreateEntry(
        YouTubePlaylistObserving observing, 
        YouTubePlaylistPayload payload,
        DateTime occuredAt)
    {
        return new YouTubePlaylistObservingEntry
        {
            ObservingId = observing.Id,
            OccuredAt = occuredAt,
            Payload = payload,
            PayloadSummary = payload.CreateSummary(),
            DiffSummary = null
        };
    }

    protected override async Task<bool> MutateObservingAsync(
        YouTubePlaylistObserving observing, 
        YouTubePlaylistObservingEntry entry,
        CancellationToken ct)
    {
        observing.PlaylistName = await GetPlaylistNameAsync(observing.PlaylistId, ct);
        await MutateUnavailableItemsAsync(observing, entry, ct);
        
        return true;
    }

    private async Task MutateUnavailableItemsAsync(
        YouTubePlaylistObserving observing,
        YouTubePlaylistObservingEntry entry,
        CancellationToken ct = default)
    {
        var unavailables = (entry.Payload as YouTubePlaylistPayload)!.Items
            .Where(x => !x.IsAvailable)
            .DistinctBy(x => x.VideoId)
            .ToList();
        var savedItems = await youtubePlaylistRepo
            .GetItems(unavailables.Select(x => x.VideoId), ct);
        
        // удаляю тех, которые стали заново доступны
        var toRemove = observing.UnavailableItems
            .Where(item => unavailables.All(x => x.VideoId != item.CurrentItem.VideoId));
        foreach (var item in toRemove)
        {
            observing.UnavailableItems.Remove(item);
        }
        
        // добавляю, что стало недоступно
        foreach (var item in unavailables)
        {
            if (observing.UnavailableItems.Any(x => x.CurrentItem.VideoId == item.VideoId))
            {
                continue;
            }
            
            TryGetSavedItem(item.VideoId, savedItems, skipUnavailable: true, out var savedItem);
            observing.UnavailableItems.Add(new UnavailableYouTubePlaylistItem
            {
                SavedItem = savedItem,
                CurrentItem = item
            });
        }
    }

    private async Task<string> GetPlaylistNameAsync(string playlistId, CancellationToken ct)
    {
        var req = youtubeService.Playlists.List("snippet");
        req.Id = playlistId;
        var response = await req.ExecuteAsync(ct);
        
        if (response.Items == null || response.Items.Count == 0)
        {
            throw new ArgumentException("Playlist not found", nameof(playlistId));
        }
        
        return response.Items[0].Snippet.Title;
    }

    private async Task<Result<List<YouTubePlaylistItem>>> GetPlaylistItemsAsync(string playlistId, CancellationToken ct)
    {
        try
        {
            List<YouTubePlaylistItem> result = [];

            var req = youtubeService.PlaylistItems.List("snippet, status");
            req.PlaylistId = playlistId;
            req.MaxResults = 50;
            var response = await req.ExecuteAsync(ct);

            while (true)
            {
                result.AddRange(response.Items.Select(item => item.ToYouTubePlaylistItem()));

                if (response.NextPageToken is not null)
                {
                    req.PageToken = response.NextPageToken;
                    response = await req.ExecuteAsync(ct);
                }
                else
                {
                    break;
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error fetching playlist items: {ex.Message}");
        }
    }

    private static bool TryGetSavedItem(
        string videoId, 
        List<YouTubePlaylistItem> savedItems, 
        bool skipUnavailable,
        [NotNullWhen(true)] out YouTubePlaylistItem? item)
    {
        // собираю несколько снапшотов одного и того же ролика
        var savedItemsWithSameVideoId = savedItems
            .Where(x => x.VideoId == videoId)
            .OrderByDescending(x => x.SavedAt)
            .ToList();

        // снапшотов нет
        if (savedItemsWithSameVideoId.Count == 0)
        {
            item = null;
            return false;
        }
            
        // последний доступный снапшот 
        var savedAvailableItem = savedItemsWithSameVideoId.FirstOrDefault(x => x.IsAvailable);
        if (savedAvailableItem != null)
        {
            item = savedAvailableItem;
            return true;
        }
        
        if (skipUnavailable)
        {
            item = null;
            return false;
        }
        
        item = savedItemsWithSameVideoId.First();
        return true;
    }
}