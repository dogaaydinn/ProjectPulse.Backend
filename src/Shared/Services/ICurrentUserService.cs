namespace Shared.Services;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? Email { get; }
    IReadOnlyList<string> Roles { get; }
    bool IsInRole(string role);
    bool IsAuthenticated { get; }
    
    event EventHandler<UserChangedEventArgs>? UserChanged;
}

public record UserChangedEventArgs(Guid? OldUserId, Guid? NewUserId);