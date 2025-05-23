namespace Shared.Services;

public record UserChangedEventArgs(Guid? OldUserId, Guid? NewUserId);