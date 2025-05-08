using FluentResults;
using MediatR;

namespace WebObserver.Main.Application.Cqrs.Queries;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;