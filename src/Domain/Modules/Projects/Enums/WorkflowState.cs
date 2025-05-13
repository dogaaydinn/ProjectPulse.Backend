using Domain.Core.Primitives.Enums.Base;

namespace Domain.Modules.Projects.Enums;

public sealed class WorkflowState : StructuredEnum<WorkflowState>
{
    public static readonly WorkflowState Active = new(nameof(Active), 1);
    public static readonly WorkflowState Inactive = new(nameof(Inactive), 2);
    public static readonly WorkflowState Archived = new(nameof(Archived), 3);

    private WorkflowState(string name, int value) : base(name, value) { }
}