using FluentResults;

namespace WebObserver.Main.Application.Features.Errors;

public record BaseError(string Message) : IError
{
    public Dictionary<string, object> Metadata { get; } = [];
    public List<IError> Reasons { get; } = [];
}