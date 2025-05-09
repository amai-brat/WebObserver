using FluentValidation;
using WebObserver.Main.Application.Features.Observings.Validators;

namespace WebObserver.Main.Application.Features.Observings.Commands.AddObserving;

public class AddObservingCommandValidator : AbstractValidator<AddObservingCommand>
{
    public AddObservingCommandValidator()
    {
        RuleFor(x => x.Request.CronExpression)
            .SetValidator(new CronExpressionValidator());
    }
}