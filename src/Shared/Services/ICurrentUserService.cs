namespace Shared.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? UserEmail { get; }
    string? Role { get; }
}