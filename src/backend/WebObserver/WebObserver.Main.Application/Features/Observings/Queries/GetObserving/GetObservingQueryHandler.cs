using FluentResults;
using WebObserver.Main.Application.Cqrs.Queries;
using WebObserver.Main.Application.Features.Errors;
using WebObserver.Main.Application.Mappers;
using WebObserver.Main.Domain.Repositories;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetObserving;

public class GetObservingQueryHandler(
    IUserRepository userRepository,
    IObservingRepository observingRepository) : IQueryHandler<GetObservingQuery, ObservingResponse>
{
    public async Task<Result<ObservingResponse>> Handle(GetObservingQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdWithObservingsAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Fail(new UserNotFoundError(request.UserId));
        }
        
        if (user.Observings.All(o => o.Id != request.ObservingId))
        {
            return Result.Fail(new ObservingNotFoundError());
        }
        
        var observing = await observingRepository.GetByIdWithEntriesSummaryAsync(request.ObservingId, cancellationToken);
        return new ObservingResponse
        {
            Observing = observing!.ToDto()
        };
    }
}