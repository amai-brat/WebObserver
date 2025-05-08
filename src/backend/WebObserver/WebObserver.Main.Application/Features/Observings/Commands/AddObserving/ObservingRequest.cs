using System.Text.Json.Serialization;
using FluentResults;
using WebObserver.Main.Application.Features.Errors;
using WebObserver.Main.Application.Helpers;
using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Application.Features.Observings.Commands.AddObserving;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(YouTubePlaylistObservingRequest), nameof(ObservingType.YouTubePlaylist))]
[JsonDerivedType(typeof(TextObservingRequest), nameof(ObservingType.Text))]
public abstract class BaseObservingRequest
{
    public required string CronExpression { get; set; }
    public required int TemplateId { get; set; }
}

public class YouTubePlaylistObservingRequest : BaseObservingRequest
{
    public string? PlaylistId { get; set; }
    public string? PlaylistUrl { get; set; }
    
    public Result<string> GetPlaylistId()
    {
        if (!string.IsNullOrEmpty(PlaylistId))
        {
            return PlaylistId;
        }

        if (string.IsNullOrEmpty(PlaylistUrl))
        {
            return Result.Fail(new PlaylistIdMissingError());
        }

        var match = RegexHelper.YouTubePlaylistIdRegex().Match(PlaylistUrl);
        return match.Success 
            ? match.Groups[1].Value 
            : Result.Fail(new PlaylistIdMissingError());
    }
}

public class TextObservingRequest : BaseObservingRequest
{
    public required string Url { get; set; }
}