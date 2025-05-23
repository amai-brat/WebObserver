using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetObservingEntryPayload;

public class ObservingEntryPayloadResponse
{
    public required ObservingPayload Payload { get; set; }
}