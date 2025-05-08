using FluentResults;
using WebObserver.Main.Application.Features.Errors;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Text;

namespace WebObserver.Main.Application.Features.Observings.Commands.AddObserving.Factories;

public sealed class TextObservingFactory : IObservingFactory
{
    public Type RequestType => typeof(TextObservingRequest);

    public Task<Result<ObservingBase>> CreateAsync(BaseObservingRequest request, ObservingTemplate template)
    {
        if (request is not TextObservingRequest textRequest)
        {
            return Task.FromResult(Result.Fail<ObservingBase>(new InvalidRequestTypeError(typeof(TextObservingRequest), request.GetType())));
        }

        if (template.Type != ObservingType.Text)
        {
            return Task.FromResult(Result.Fail<ObservingBase>(new TemplateTypeMismatchError(ObservingType.Text, template.Type)));
        }

        var observing = new TextObserving(template, request.CronExpression, textRequest.Url);
        return Task.FromResult(Result.Ok<ObservingBase>(observing));
    }
}
