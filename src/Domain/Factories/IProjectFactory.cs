using Domain.Entities;

namespace Domain.Factories;

public interface IProjectFactory
{
    Project Create(string name, string? description, DateTime startDate, DateTime? endDate, Guid managerId);
}