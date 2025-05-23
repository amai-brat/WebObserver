using FluentResults;
using WebObserver.Main.Application.Cqrs.Queries;
using WebObserver.Main.Application.Features.Errors;
using WebObserver.Main.Domain.Repositories;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetObservingEntryDiffPayload;

public class GetObservingEntryDiffPayloadQueryHandler(
    IUserRepository userRepository,
    IObservingRepository observingRepository) : IQueryHandler<GetObservingEntryDiffPayloadQuery, ObservingEntryDiffPayloadResponse>
{
    public async Task<Result<ObservingEntryDiffPayloadResponse>> Handle(GetObservingEntryDiffPayloadQuery request, CancellationToken cancellationToken)
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
        
        var entry = await observingRepository.GetEntryAsync(request.ObservingId, request.EntryId, cancellationToken);
        if (entry is null)
        {
            return Result.Fail(new ObservingEntryNotFoundError());
        }
        
        return new ObservingEntryDiffPayloadResponse
        {
            Payload = entry.LastDiff?.Payload
        };
    }
}