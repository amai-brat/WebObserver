using WebObserver.Main.Domain.Base;

namespace WebObserver.Main.Application.Features.Errors;

public sealed class UserNotFoundError(int userId) : BaseError($"User not found with ID: {userId}");
public sealed class TemplateNotFoundError(int templateId) : BaseError($"Template not found with ID: {templateId}");
public sealed class TemplateTypeMismatchError(ObservingType expected, ObservingType actual) 
    : BaseError($"Invalid template type. Expected: {expected}, Actual: {actual}");
public sealed class InvalidRequestTypeError(Type expected, Type actual) 
    : BaseError($"Invalid request type. Expected: {expected.Name}, Actual: {actual.Name}");
public sealed class PlaylistIdMissingError() : BaseError("Playlist URL or ID is required");
public sealed class InvalidPlaylistUrlError() : BaseError("Couldn't extract playlist ID from URL");
public sealed class ObservingNotFoundError() : BaseError("Observing not found");
public sealed class ObservingEntryNotFoundError() : BaseError("Observing entry not found");