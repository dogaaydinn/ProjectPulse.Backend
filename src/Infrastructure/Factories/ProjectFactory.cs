using Domain.Entities;
using Domain.Factories;

namespace Infrastructure.Factories;

public class ProjectFactory : IProjectFactory
{
    public Project Create(string name, string? description, DateTime startDate, DateTime? endDate, Guid managerId)
    {
        return new Project(name, description, startDate, endDate, managerId);
    }
}