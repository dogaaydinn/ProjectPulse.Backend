using Domain.Core.ValueObjects;
using Domain.Modules.Tasks.Entities;
using Domain.Modules.Tasks.Enums;
using Shared.Base;
using Shared.Constants;
using Shared.Exceptions;

namespace Domain.Modules.Projects.Entities;

public class Status : BaseEntity
{
    public LocalizedString Name { get; private set; } = null!;
    public StatusType Type { get; private set; } = StatusType.NotStarted;
    public string? Color { get; private set; }
    public int Order { get; private set; }

    public Guid WorkflowId { get; private set; }
    public Workflow Workflow { get; private set; } = null!;

    public ICollection<TaskItem> Tasks { get; private set; } = new List<TaskItem>();

    protected Status() { }

    public Status(LocalizedString name, StatusType type, int order, Guid workflowId, string? color = null)
    {
        SetName(name);
        Type = type;
        Order = order;
        WorkflowId = workflowId;
        Color = color;
    }

    public void SetName(LocalizedString name)
    {
        if (name is null || name.IsEmpty())
            throw new AppException(ErrorCodes.Validation, ValidationMessages.Status.NameRequired);

        Name = name;
    }

    public void SetColor(string? color)
    {
        Color = color?.Trim();
    }

    public void ChangeType(StatusType type)
    {
        Type = type;
    }

    public void Reorder(int newOrder)
    {
        if (newOrder < 0)
            throw new AppException(ErrorCodes.Validation, ValidationMessages.Status.OrderMustBeNonNegative);

        Order = newOrder;
    }
}