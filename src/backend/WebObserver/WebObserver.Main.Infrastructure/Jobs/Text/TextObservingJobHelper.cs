using FluentResults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebObserver.Main.Application.Services.Ifaces;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Repositories;
using WebObserver.Main.Domain.Services;
using WebObserver.Main.Domain.Text;

namespace WebObserver.Main.Infrastructure.Jobs.Text;

public sealed class TextObservingJobHelper(
    [FromKeyedServices(nameof(ObservingType.Text))] IDiffGenerator diffGenerator,
    IObservingRepository observingRepo,
    IHttpClientFactory httpClientFactory,
    ILogger<TextObservingJobHelper> logger,
    INotifier notifier,
    IMessageFactory messageFactory,
    IUnitOfWork unitOfWork)
    : ObservingJobHelperBase<TextObserving, TextObservingEntry, TextPayload>(
        observingRepo,
        logger,
        notifier,
        messageFactory,
        diffGenerator,
        unitOfWork)
{
    protected override async Task<Result<TextPayload>> FetchPayloadAsync(
        TextObserving observing,
        CancellationToken ct)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient();
            var text = await httpClient.GetStringAsync(observing.Url, ct);
            return new TextPayload { Text = text };
        }
        catch (Exception ex)
        {
            return Result.Fail<TextPayload>($"Text fetch failed: {ex.Message}");
        }
    }

    protected override TextObservingEntry CreateEntry(
        TextObserving observing,
        TextPayload payload,
        DateTime occuredAt) =>
        new()
        {
            ObservingId = observing.Id,
            OccuredAt = occuredAt,
            Payload = payload,
            PayloadSummary = payload.CreateSummary(),
            DiffSummary = null
        };
}