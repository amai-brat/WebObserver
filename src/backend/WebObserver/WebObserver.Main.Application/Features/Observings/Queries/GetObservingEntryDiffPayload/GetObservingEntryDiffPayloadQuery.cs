using WebObserver.Main.Application.Cqrs.Queries;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetObservingEntryDiffPayload;

public record GetObservingEntryDiffPayloadQuery(int UserId, int ObservingId, int EntryId) 
    : IQuery<ObservingEntryDiffPayloadResponse>;