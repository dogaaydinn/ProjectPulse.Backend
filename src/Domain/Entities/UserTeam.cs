using Shared.Base;
using Shared.Exceptions;

namespace Domain.Entities;

public class UserTeam : BaseEntity
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public Guid TeamId { get; private set; }
    public Team Team { get; private set; } = null!;

    public DateTime JoinedAt { get; private set; }
    public string? Role { get; private set; } // Optional: "Member", "Admin"

    protected UserTeam() { }

    public UserTeam(Guid userId, Guid teamId, string? role = "Member")
    {
        if (userId == Guid.Empty || teamId == Guid.Empty)
            throw new AppException("Validation.UserTeam.Invalid", "User and Team IDs must be valid.");

        UserId = userId;
        TeamId = teamId;
        Role = role;
        JoinedAt = DateTime.UtcNow;
    }

    public void UpdateRole(string newRole)
    {
        Role = newRole;
    }
}