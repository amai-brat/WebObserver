using WebObserver.Main.Application.Cqrs.Commands;

namespace WebObserver.Main.Application.Features.Observings.Commands.RemoveObserving;

public record RemoveObservingCommand(int UserId, int ObservingId) : ICommand;