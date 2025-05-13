using Domain.Modules.Projects.Enums;
using Domain.Modules.Projects.ValueObjects;
using Shared.Base;
using Shared.Constants;
using Shared.Exceptions;
using Shared.Validation;
using Shared.ValueObjects;

namespace Domain.Modules.Projects.Entities;

public class Milestone : BaseAuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public MilestoneSchedule Schedule { get; private set; } = null!;
    public MilestoneStatus Status { get; private set; } = MilestoneStatus.Planned;

    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; } = null!;

    protected Milestone() { }

    internal Milestone(string name, MilestoneSchedule schedule, Guid projectId, string? description = null)
    {
        Guard.AgainstEmpty(name, ErrorCodes.Validation, ValidationMessages.Milestone.NameRequired);
        Guard.AgainstDefaultGuid(projectId, ErrorCodes.Validation, ValidationMessages.Common.ProjectIdRequired);

        Name = name.Trim();
        Schedule = schedule;
        ProjectId = projectId;
        Description = description;
    }

    public void Update(string name, string? description, MilestoneSchedule newSchedule)
    {
        Guard.AgainstEmpty(name, ErrorCodes.Validation, ValidationMessages.Milestone.NameRequired);

        Name = name.Trim();
        Description = description;
        Schedule = newSchedule;
    }

    public void MarkComplete()
    {
        if (Status == MilestoneStatus.Completed)
            throw new AppException(ErrorCodes.Validation, ValidationMessages.Milestone.AlreadyCompleted);

        Status = MilestoneStatus.Completed;
    }

    public bool IsLate() => Schedule.IsOverdue();
}