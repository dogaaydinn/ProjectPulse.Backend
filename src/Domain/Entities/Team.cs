using Shared.Base;
using Shared.Exceptions;

namespace Domain.Entities;

public class Team : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    public ICollection<UserTeam> UserTeams { get; private set; } = new List<UserTeam>();
    public ICollection<TeamProject> TeamProjects { get; private set; } = new List<TeamProject>();

    protected Team() { }

    public Team(string name, string? description = null)
    {
        SetName(name);
        Description = description;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new AppException("Validation.Team.Name", "Team name cannot be empty.");
        Name = name.Trim();
    }

    public void UpdateDescription(string? description)
    {
        Description = description;
    }
}