using FluentResults;
using WebObserver.Main.Application.Cqrs.Queries;
using WebObserver.Main.Application.Features.Errors;
using WebObserver.Main.Application.Mappers;
using WebObserver.Main.Domain.Repositories;

namespace WebObserver.Main.Application.Features.Observings.Queries.GetAllObservings;

public class GetAllObservingsQueryHandler(
    IUserRepository userRepository): IQueryHandler<GetAllObservingsQuery, ObservingsResponse>
{
    public async Task<Result<ObservingsResponse>> Handle(GetAllObservingsQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdWithObservingsAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Fail(new UserNotFoundError(request.UserId));
        }

        var dtos = user.Observings
            .Select(x => x.ToDto())
            .ToList();

        return new ObservingsResponse
        {
            Observings = dtos
        };
    }
}