using FluentResults;
using WebObserver.Main.Application.Cqrs.Queries;
using WebObserver.Main.Application.Features.Errors;
using WebObserver.Main.Domain.Repositories;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetObservingEntryPayload;

public class GetObservingEntryPayloadQueryHandler(
    IUserRepository userRepository,
    IObservingRepository observingRepository) : IQueryHandler<GetObservingEntryPayloadQuery, ObservingEntryPayloadResponse>
{
    public async Task<Result<ObservingEntryPayloadResponse>> Handle(GetObservingEntryPayloadQuery request, CancellationToken cancellationToken)
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

        return new ObservingEntryPayloadResponse
        {
            Payload = null
        };
    }
}