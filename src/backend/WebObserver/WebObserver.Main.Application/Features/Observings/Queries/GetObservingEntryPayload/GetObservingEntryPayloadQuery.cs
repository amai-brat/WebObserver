using WebObserver.Main.Application.Cqrs.Queries;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetObservingEntryPayload;

public record GetObservingEntryPayloadQuery(int UserId, int ObservingId, int EntryId) 
    : IQuery<ObservingEntryPayloadResponse>;