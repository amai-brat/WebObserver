using FluentResults;
using MediatR;

namespace WebObserver.Main.Application.Cqrs.Commands;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;