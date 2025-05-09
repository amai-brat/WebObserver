using FluentValidation;
using WebObserver.Main.Application.Features.Observings.Validators;

namespace WebObserver.Main.Application.Features.Observings.Commands.EditObserving;

public class EditObservingCommandValidator : AbstractValidator<EditObservingCommand>
{
    public EditObservingCommandValidator()
    {
        RuleFor(x => x.Request.CronExpression)
            .SetValidator(new CronExpressionValidator());
    }
}