using Shared.Base;
using Shared.Exceptions;
using Shared.Constants;

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
        Validate(teamId, projectId);
        TeamId = teamId;
        ProjectId = projectId;
    }

    private static void Validate(Guid teamId, Guid projectId)
    {
        if (teamId == Guid.Empty)
            throw new AppException(
                ErrorCodes.Validation,
                ValidationMessages.TeamProject.TeamIdRequired);

        if (projectId == Guid.Empty)
            throw new AppException(
                ErrorCodes.Validation,
                ValidationMessages.Common.ProjectIdRequired);
    }
}