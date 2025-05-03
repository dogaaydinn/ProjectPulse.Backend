using Domain.Enums;
using Shared.Base;
using Shared.Exceptions;

namespace Domain.Entities;

public class Status : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public StatusType Type { get; private set; } = StatusType.NotStarted;
    public string? Color { get; private set; }
    public int Order { get; private set; }

    public Guid WorkflowId { get; private set; }
    public Workflow Workflow { get; private set; } = null!;

    public ICollection<TaskItem> Tasks { get; private set; } = new List<TaskItem>();

    protected Status() { }

    public Status(string name, StatusType type, int order, Guid workflowId, string? color = null)
    {
        SetName(name);
        Type = type;
        Order = order;
        WorkflowId = workflowId;
        Color = color;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new AppException("Validation.Status.Name", "Status name cannot be empty.");
        Name = name;
    }

    public void SetColor(string? color)
    {
        Color = color;
    }

    public void ChangeType(StatusType type)
    {
        Type = type;
    }

    public void Reorder(int newOrder)
    {
        Order = newOrder;
    }
}