using Domain.Modules.Projects.Entities;
using Shared.Base;
using Shared.Exceptions;

namespace Domain.Modules.Tasks.Entities;

public class TaskLabel : BaseEntity
{
    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; } = null!;

    public Guid LabelId { get; private set; }
    public Label Label { get; private set; } = null!;

    protected TaskLabel() { }

    internal TaskLabel(Guid taskItemId, Guid labelId)
    {
        if (taskItemId == Guid.Empty || labelId == Guid.Empty)
            throw new AppException("Validation.TaskLabel.Invalid", "Task and Label IDs must be valid.");

        TaskItemId = taskItemId;
        LabelId = labelId;
    }
}