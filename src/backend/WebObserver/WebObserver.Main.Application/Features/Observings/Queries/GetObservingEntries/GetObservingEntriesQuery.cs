using WebObserver.Main.Application.Cqrs.Queries;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetObservingEntries;

public record GetObservingEntriesQuery(int UserId, int ObservingId, int Page, int PageSize) : IQuery<ObservingEntriesResponse>;