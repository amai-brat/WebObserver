using WebObserver.Main.Application.Cqrs.Queries;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetObserving;

public record GetObservingQuery(int UserId, int ObservingId) : IQuery<ObservingResponse>;