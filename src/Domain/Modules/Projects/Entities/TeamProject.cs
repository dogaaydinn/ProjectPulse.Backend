using Domain.Modules.Teams.Entities;
using Shared.Base;
using Shared.Constants;
using Shared.Validation;

namespace Domain.Modules.Projects.Entities;

public class TeamProject : BaseAuditableEntity
{
    public Guid TeamId { get; private set; }
    public Team Team { get; private set; } = null!;

    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; } = null!;

    protected TeamProject() { }

    internal TeamProject(Guid teamId, Guid projectId)
    {
        Guard.AgainstDefaultGuid(teamId, ErrorCodes.Validation, ValidationMessages.TeamProject.TeamIdRequired);
        Guard.AgainstDefaultGuid(projectId, ErrorCodes.Validation, ValidationMessages.Common.ProjectIdRequired);

        TeamId = teamId;
        ProjectId = projectId;
    }
}