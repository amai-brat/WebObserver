using WebObserver.Main.Application.Cqrs.Commands;

namespace WebObserver.Main.Application.Features.Observings.Commands.EditObserving;

public record EditObservingCommand(int UserId, int ObservingId, EditObservingRequest Request) : ICommand;

public class EditObservingRequest
{
    public string? CronExpression { get; set; }
}