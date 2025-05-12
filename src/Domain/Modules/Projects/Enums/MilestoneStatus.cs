using Domain.Core.Primitives.Enums.Base;

namespace Domain.Modules.Projects.Enums;

public sealed class MilestoneStatus : StructuredEnum<MilestoneStatus, int>
{
    public static readonly MilestoneStatus Planned = new(nameof(Planned), 1);
    public static readonly MilestoneStatus InProgress = new(nameof(InProgress), 2);
    public static readonly MilestoneStatus Completed = new(nameof(Completed), 3);
    public static readonly MilestoneStatus Delayed = new(nameof(Delayed), 4);

    private MilestoneStatus(string name, int value) : base(name, value) { }

    public bool IsFinal => this == Completed || this == Delayed;
}