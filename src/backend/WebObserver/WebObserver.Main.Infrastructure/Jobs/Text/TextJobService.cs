using FluentResults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebObserver.Main.Domain.Repositories;
using WebObserver.Main.Domain.Services;
using WebObserver.Main.Domain.Text;

namespace WebObserver.Main.Infrastructure.Jobs.Text;

public class TextJobService(IServiceScopeFactory scopeFactory) : IJobService
{
    public async Task ObserveAsync(int observingId, CancellationToken cancellationToken = default)
    {
        await using (var scope = scopeFactory.CreateAsyncScope())
        {
            var observingRepo = scope.ServiceProvider.GetRequiredService<IObservingRepository>();
            var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<TextJobService>>();
            var diffGenerator = scope.ServiceProvider.GetRequiredService<IDiffGenerator<TextPayload, TextDiffPayload>>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            
            logger.LogInformation("Observing Text {Id} at {Time}", observingId, DateTime.UtcNow);
            
            var observing = await observingRepo.GetByIdAsync(observingId, cancellationToken);
            if (observing is not TextObserving textObserving)
            {
                throw new InvalidOperationException($"{GetType().Name} is used for {observing?.GetType().Name ?? "null"}");
            }

            var httpClient = httpClientFactory.CreateClient();
            var entryResult = await CreateEntryAsync(httpClient, textObserving, observingRepo, diffGenerator, cancellationToken);
            if (entryResult.IsFailed)
            {
                logger.LogWarning("Couldn't create text entry at {Time} with {Error}", DateTime.UtcNow, string.Join("\n", entryResult.Errors));
            }
            
            textObserving.AddEntry(entryResult.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    private static async Task<Result<TextObservingEntry>> CreateEntryAsync(
        HttpClient httpClient,
        TextObserving textObserving,
        IObservingRepository observingRepo,
        IDiffGenerator<TextPayload, TextDiffPayload> diffGenerator,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var text = await httpClient.GetStringAsync(textObserving.Url, cancellationToken);

            var entry = new TextObservingEntry
            {
                ObservingId = textObserving.Id,
                OccuredAt = DateTime.UtcNow,
                Payload = new TextPayload
                {
                    Text = text
                }
            };
            
            var lastEntry = await observingRepo.GetLastEntryByObservingIdAsync<TextPayload>(textObserving.Id, cancellationToken);
            var diff = diffGenerator.GenerateDiff(lastEntry, entry);
            
            entry.LastDiff = diff;
            
            return Result.Ok(entry);
        }
        catch (Exception e)
        {
            return Result.Fail<TextObservingEntry>($"Error fetching text: {e.Message}");
        }
    }
}