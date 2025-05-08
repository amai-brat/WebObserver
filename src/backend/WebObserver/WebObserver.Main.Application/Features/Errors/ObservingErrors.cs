using FluentResults;
using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Application.Features.Errors;

public sealed record UserNotFoundError(int UserId) : BaseError($"User not found with ID: {UserId}");
public sealed record TemplateNotFoundError(int TemplateId) : BaseError($"Template not found with ID: {TemplateId}");
public sealed record TemplateTypeMismatchError(ObservingType Expected, ObservingType Actual) 
    : BaseError($"Invalid template type. Expected: {Expected}, Actual: {Actual}");
public sealed record InvalidRequestTypeError(Type Expected, Type Actual) 
    : BaseError($"Invalid request type. Expected: {Expected.Name}, Actual: {Actual.Name}");
public sealed record PlaylistIdMissingError() : BaseError("Playlist URL or ID is required");
public sealed record InvalidPlaylistUrlError() : BaseError("Couldn't extract playlist ID from URL");