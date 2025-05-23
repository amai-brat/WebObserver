using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetObservingEntryDiffPayload;

public class ObservingEntryDiffPayloadResponse
{
    public required DiffPayload? Payload { get; set; }
}