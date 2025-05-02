using Domain.Entities;
using Domain.Factories;

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
        var project = new Project(name, description, startDate, endDate, managerId);
        return project;
    }
}