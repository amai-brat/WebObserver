using FluentResults;

namespace WebObserver.Main.Application.Features.Errors;

public class BaseError(string message) : IError
{
    public string Message { get; } = message;
    public Dictionary<string, object> Metadata { get; } = [];
    public List<IError> Reasons { get; } = [];
}