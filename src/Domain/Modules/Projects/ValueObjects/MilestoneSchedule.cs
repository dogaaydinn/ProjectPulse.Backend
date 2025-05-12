using Domain.Core.ValueObjects;
using Shared.Exceptions;

namespace Domain.Modules.Projects.ValueObjects;

public sealed class MilestoneSchedule : ValueObject
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    private MilestoneSchedule() { }

    public MilestoneSchedule(DateTime startDate, DateTime endDate)
    {
        if (startDate == default)
            throw new AppException("Validation.MilestoneSchedule.Start", "Start date cannot be empty.");

        if (endDate == default)
            throw new AppException("Validation.MilestoneSchedule.End", "End date cannot be empty.");

        if (endDate < startDate)
            throw new AppException("Validation.MilestoneSchedule.Range", "End date must be after start date.");

        StartDate = startDate;
        EndDate = endDate;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }

    public bool IsOverdue() => EndDate < DateTime.UtcNow;
}