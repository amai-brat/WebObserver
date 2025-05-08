using WebObserver.Main.Application.Cqrs.Commands;

namespace WebObserver.Main.Application.Features.Observings.Commands.AddObserving;

public record AddObservingCommand(int UserId, BaseObservingRequest Request) : ICommand<AddObservingResponse>;