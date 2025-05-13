using Domain.Core.Primitives.Enums.Base;

namespace Domain.Modules.Users.Enums;

public sealed class TeamRole : StructuredEnum<TeamRole>
{
    public static readonly TeamRole Member = new(nameof(Member), 1);
    public static readonly TeamRole TeamAdmin = new(nameof(TeamAdmin), 2);
    public static readonly TeamRole Viewer = new(nameof(Viewer), 3);

    private TeamRole(string name, int value) : base(name, value) { }
}