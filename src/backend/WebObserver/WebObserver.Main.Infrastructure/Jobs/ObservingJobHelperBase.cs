using FluentResults;
using Microsoft.Extensions.Logging;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Repositories;
using WebObserver.Main.Domain.Services;

namespace WebObserver.Main.Infrastructure.Jobs;

public abstract class ObservingJobHelperBase<TObserving, TEntry, TPayload>(
    IObservingRepository observingRepo,
    ILogger logger,
    INotifier notifier,
    IDiffGenerator diffGenerator,
    IUnitOfWork unitOfWork)
    where TObserving : ObservingBase
    where TEntry : ObservingEntryBase
    where TPayload : class
{
    public async Task ObserveInternalAsync(int observingId, CancellationToken ct)
    {
        logger.LogInformation("Observing {Type} {Id}", typeof(TObserving).Name, observingId);
        
        if (await observingRepo.GetByIdWithUserAsync(observingId, ct) is not TObserving observing)
        {
            logger.LogWarning("Invalid observing entity for ID {Id}", observingId);
            return;
        }
        
        var payloadResult = await FetchPayloadAsync(observing, ct);
        if (payloadResult.IsFailed)
        {
            logger.LogWarning("Fetch {PayloadType} failed: {@Errors}", typeof(TPayload), payloadResult.Errors);
            return;
        }
        // получить до SaveChanges, т.к. иначе станет предпоследним 
        var prevEntry = await observingRepo.GetLastEntryByObservingIdAsync(observingId, ct);
        
        var now = DateTime.UtcNow;
        var entry = CreateEntry(observing, payloadResult.Value, now);
        observing.AddEntry(entry);
        await unitOfWork.SaveChangesAsync(ct);

        // два SaveChanges, потому что циклическая зависимость между Entry.Id <=> Diff.EntryId 
        AttachDiffToEntry(prevEntry, entry, observing);
        observing.LastEntryAt = now;
        await unitOfWork.SaveChangesAsync(ct);
        
        // дочерние классы переопределяют этот метод, если необходимо
        var save = await MutateObservingAsync(observing, entry, ct);
        if (save)
        {
            await unitOfWork.SaveChangesAsync(ct);
        }

        if (NeedToNotify(entry))
        {
            await notifier.NotifyAsync(observing.User, Message.Create(observing.User, observing), ct);
        }
    }

    private void AttachDiffToEntry(ObservingEntryBase? prevEntry, TEntry currentEntry, TObserving observing)
    {
        var diff = diffGenerator.GenerateDiff(prevEntry, currentEntry);
        currentEntry.LastDiff = diff;
        
        if (diff is { Payload.IsEmpty: false })
            observing.LastChangeAt = DateTime.UtcNow;
        
        currentEntry.DiffSummary = diff?.Payload.CreateSummary();
    }

    protected abstract Task<Result<TPayload>> FetchPayloadAsync(
        TObserving observing,
        CancellationToken ct);

    protected abstract TEntry CreateEntry(TObserving observing, TPayload payload, DateTime occuredAt);

    protected virtual bool NeedToNotify(TEntry entry) => 
        entry.LastDiff is { Payload.IsEmpty: false };

    /// <summary>
    /// Дополнительно изменить специфичные для типа <paramref name="observing"/> свойства
    /// </summary>
    /// <param name="observing">Наблюдение</param>
    /// <param name="entry">Текущая запись наблюдения</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>bool - необходимо ли сохранять в базе данных</returns>
    protected virtual Task<bool> MutateObservingAsync(TObserving observing, TEntry entry, CancellationToken ct)
    {
        return Task.FromResult(false);
    }
}