using FluentResults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Repositories;
using WebObserver.Main.Domain.Services;
using WebObserver.Main.Domain.Text;

namespace WebObserver.Main.Infrastructure.Jobs.Text;

public class TextJobService(IServiceScopeFactory scopeFactory) : IJobService
{
    private static bool NeedToNotify(TextObservingEntry entry)
    {
        if (entry.LastDiff is not { } diff)
        {
            return false;
        }
        
        return !diff.Payload.IsEmpty;
    }
    
    public async Task ObserveAsync(int observingId, CancellationToken cancellationToken = default)
    {
        await using (var scope = scopeFactory.CreateAsyncScope())
        {
            var observingRepo = scope.ServiceProvider.GetRequiredService<IObservingRepository>();
            var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<TextJobService>>();
            var notifier = scope.ServiceProvider.GetRequiredService<INotifier>();
            var diffGenerator = scope.ServiceProvider.GetRequiredService<IDiffGenerator<TextPayload, TextDiffPayload>>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            
            logger.LogInformation("Observing Text {Id} at {Time}", observingId, DateTime.UtcNow);
            
            var observing = await observingRepo.GetByIdWithUserAsync(observingId, cancellationToken);
            if (observing is not TextObserving textObserving)
            {
                throw new InvalidOperationException($"{GetType().Name} is used for {observing?.GetType().Name ?? "null"}");
            }

            var httpClient = httpClientFactory.CreateClient();
            var entryResult = await CreateEntryAsync(httpClient, textObserving, cancellationToken);
            if (entryResult.IsFailed)
            {
                logger.LogWarning("Couldn't create text entry at {Time} with {Error}", DateTime.UtcNow, string.Join("\n", entryResult.Errors));
                return;
            }
            
            var prevEntry = await observingRepo.GetLastEntryByObservingIdAsync<TextPayload, TextDiffPayload>(textObserving.Id, cancellationToken);
            textObserving.AddEntry(entryResult.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // два SaveChanges, потому что циклическая зависимость между Entry.Id <=> Diff.EntryId 
            CreateAndAttachDiff(diffGenerator, prevEntry, entryResult.Value, observing);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            if (NeedToNotify(entryResult.Value))
            {
                await notifier.NotifyAsync(observing.User, Message.Create(observing.User, observing), cancellationToken);
            }
        }
    }

    private static async Task<Result<TextObservingEntry>> CreateEntryAsync(
        HttpClient httpClient,
        TextObserving textObserving,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var text = await httpClient.GetStringAsync(textObserving.Url, cancellationToken);

            var payload = new TextPayload
            {
                Text = text
            };
            
            var entry = new TextObservingEntry
            {
                ObservingId = textObserving.Id,
                OccuredAt = DateTime.UtcNow,
                Payload = payload,
                PayloadSummary = payload.CreateSummary(),
                DiffSummary = null
            };
            
            return Result.Ok(entry);
        }
        catch (Exception e)
        {
            return Result.Fail<TextObservingEntry>($"Error fetching text: {e.Message}");
        }
    }
    
    private static void CreateAndAttachDiff(
        IDiffGenerator<TextPayload, TextDiffPayload> diffGenerator, 
        ObservingEntry<TextPayload, TextDiffPayload>? prevEntry, 
        TextObservingEntry currentEntry,
        ObservingBase observing)
    {
        var diff = diffGenerator.GenerateDiff(prevEntry, currentEntry);
        currentEntry.LastDiff = diff;
            
        observing.LastEntryAt = DateTime.UtcNow;
        if (currentEntry.LastDiff is { Payload.IsEmpty: false })
        {
            observing.LastChangeAt = DateTime.UtcNow;
        }
        currentEntry.DiffSummary = diff?.Payload.CreateSummary();
    }
}