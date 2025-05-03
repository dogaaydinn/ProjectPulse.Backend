using Shared.Base;
using Shared.Exceptions;

namespace Domain.Entities;

public class Workflow : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public bool IsDefault { get; private set; } = false;
    public bool IsActive { get; private set; } = true;

    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; } = null!;

    public ICollection<Status> Statuses { get; private set; } = new List<Status>();

    protected Workflow() { }

    public Workflow(string name, Guid projectId, bool isDefault = false)
    {
        SetName(name);
        ProjectId = projectId;
        IsDefault = isDefault;
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new AppException("Validation.Workflow.Name", "Workflow name is required.");
        Name = name;
    }

    public void ToggleDefault(bool isDefault)
    {
        IsDefault = isDefault;
    }

    public void AddStatus(Status status)
    {
        if (Statuses.Any(s => s.Type == status.Type))
            throw new AppException("Workflow.DuplicateStatus", "Workflow already contains this status.");
        Statuses.Add(status);
    }
}