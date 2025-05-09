using Cronos;
using FluentValidation;

namespace WebObserver.Main.Application.Features.Observings.Validators;

public class CronExpressionValidator : AbstractValidator<string?>
{
    public CronExpressionValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("CronExpression is required");

        RuleFor(x => x)
            .Must(str => CronExpression.TryParse(str, out _))
            .WithMessage("CronExpression is invalid");
    }
}