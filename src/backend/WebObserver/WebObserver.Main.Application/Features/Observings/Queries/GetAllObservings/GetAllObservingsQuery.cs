using WebObserver.Main.Application.Cqrs.Queries;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetAllObservings;

public record GetAllObservingsQuery(int UserId) : IQuery<ObservingsResponse>;