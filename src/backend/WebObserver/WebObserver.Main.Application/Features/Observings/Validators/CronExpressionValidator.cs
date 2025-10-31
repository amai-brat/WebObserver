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
        
        RuleFor(x => x)
            .Must(str =>
            {
                var expr = CronExpression.Parse(str);
                var occurrences = expr
                    .GetOccurrences(DateTime.UtcNow.Date, DateTime.UtcNow.Date.AddDays(1))
                    .Count();

                return occurrences <= 2; 
            })
            .WithMessage("CronExpression can occur at most two times a day");
    }
}