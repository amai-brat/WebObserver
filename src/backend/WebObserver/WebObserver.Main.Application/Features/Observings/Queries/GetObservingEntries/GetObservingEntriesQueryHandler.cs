using FluentResults;
using WebObserver.Main.Application.Cqrs.Queries;
using WebObserver.Main.Application.Features.Errors;
using WebObserver.Main.Application.Mappers;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Repositories;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetObservingEntries;

public class GetObservingEntriesQueryHandler(
    IUserRepository userRepository,
    IObservingRepository observingRepository) : IQueryHandler<GetObservingEntriesQuery, ObservingEntriesResponse>
{
    public async Task<Result<ObservingEntriesResponse>> Handle(GetObservingEntriesQuery request, CancellationToken cancellationToken)
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
        
        var entries = await observingRepository.GetEntriesAsync(request.ObservingId, cancellationToken);
        return new ObservingEntriesResponse
        {
            Entries = entries?
                .Select(x => x.ToDto())
                .ToList() ?? []
        };
    }
}