using Shared.Base;
using Shared.Exceptions;

namespace Domain.Entities;

public class TeamProject : BaseEntity
{
    public Guid TeamId { get; private set; }
    public Team Team { get; private set; } = null!;

    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; } = null!;

    protected TeamProject() { }

    internal TeamProject(Guid teamId, Guid projectId)
    {
        if (teamId == Guid.Empty || projectId == Guid.Empty)
            throw new AppException("Validation.TeamProject.InvalidIds", "Team and Project IDs must be valid.");

        TeamId = teamId;
        ProjectId = projectId;
    }
}