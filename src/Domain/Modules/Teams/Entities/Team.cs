using Domain.Modules.Projects.Entities;
using Domain.Modules.Users.Entities;
using Shared.Base;
using Shared.Constants;
using Shared.Validation;
using Shared.ValueObjects;

namespace Domain.Modules.Teams.Entities;

public class Team : BaseAuditableEntity
{
    public LocalizedString Name { get; private set; } = null!;
    public LocalizedString? Description { get; private set; }

    public ICollection<UserTeam> UserTeams { get; private set; } = new List<UserTeam>();
    public ICollection<TeamProject> TeamProjects { get; private set; } = new List<TeamProject>();

    protected Team() { }

    public Team(LocalizedString name, LocalizedString? description = null)
    {
        SetName(name);
        Description = description;
    }

    private void SetName(LocalizedString name)
    {
        Guard.AgainstEmptyLocalized(name, ErrorCodes.Validation, ValidationMessages.Team.TeamNameRequired);
        Name = name;
    }

    public void UpdateName(LocalizedString name)
    {
        SetName(name);
    }

    public void UpdateDescription(LocalizedString? description)
    {
        Description = description;
    }
}