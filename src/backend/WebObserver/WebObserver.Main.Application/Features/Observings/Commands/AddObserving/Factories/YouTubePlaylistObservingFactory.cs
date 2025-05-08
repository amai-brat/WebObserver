using FluentResults;
using WebObserver.Main.Application.Features.Errors;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.YouTubePlaylist;

namespace WebObserver.Main.Application.Features.Observings.Commands.AddObserving.Factories;

public sealed class YouTubePlaylistObservingFactory : IObservingFactory
{
    public Type RequestType => typeof(YouTubePlaylistObservingRequest);

    public Task<Result<ObservingBase>> CreateAsync(BaseObservingRequest request, ObservingTemplate template)
    {
        if (request is not YouTubePlaylistObservingRequest youtubeRequest)
        {
            return Task.FromResult(Result.Fail<ObservingBase>(new InvalidRequestTypeError(typeof(YouTubePlaylistObservingRequest), request.GetType())));
        }

        if (template.Type != ObservingType.YouTubePlaylist)
        {
            return Task.FromResult(Result.Fail<ObservingBase>(new TemplateTypeMismatchError(ObservingType.YouTubePlaylist, template.Type)));
        }

        var playlistIdResult = youtubeRequest.GetPlaylistId();
        if (playlistIdResult.IsFailed)
        {
            return Task.FromResult(Result.Fail<ObservingBase>(playlistIdResult.Errors));
        }

        var observing = new YouTubePlaylistObserving(template, request.CronExpression, playlistIdResult.Value);
        return Task.FromResult(Result.Ok<ObservingBase>(observing));
    }
}
