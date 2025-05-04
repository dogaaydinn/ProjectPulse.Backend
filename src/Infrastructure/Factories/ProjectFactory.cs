using Domain.Entities;
using Domain.Factories;
using Shared.Constants;
using Shared.Exceptions;

namespace Infrastructure.Factories;

public class ProjectFactory : IProjectFactory
{
    public Project Create(
        string name,
        string? description,
        DateTime startDate,
        DateTime? endDate,
        Guid managerId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new AppException(ErrorCodes.Validation, ValidationMessages.Project.ProjectNameRequired);

        if (managerId == Guid.Empty)
            throw new AppException(ErrorCodes.Validation, ValidationMessages.Project.ManagerIdRequired);

        if (endDate.HasValue && endDate < startDate)
            throw new AppException(ErrorCodes.Validation, ValidationMessages.Common.StartDateMustBeBeforeEndDate);
        var project = new Project(name, description, startDate, endDate, managerId);
        return project;
    }
}