using Domain.Modules.Teams.Entities;
using Domain.Modules.Users.Enums;
using Shared.Base;
using Shared.Exceptions;

namespace Domain.Modules.Users.Entities;

public class UserTeam : BaseEntity
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public Guid TeamId { get; private set; }
    public Team Team { get; private set; } = null!;

    public TeamRole Role { get; private set; } = TeamRole.Member;
    public DateTime JoinedAt { get; private set; }

    protected UserTeam() { }

    public UserTeam(Guid userId, Guid teamId, TeamRole role)
    {
        if (userId == Guid.Empty || teamId == Guid.Empty)
            throw new AppException("Validation.UserTeam.Invalid", "User and Team IDs must be valid.");

        UserId = userId;
        TeamId = teamId;
        Role = role;
        JoinedAt = DateTime.UtcNow;
    }

    public void UpdateRole(TeamRole newRole)
    {
        Role = newRole;
    }
}