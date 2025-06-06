using Domain.Modules.Projects.Entities;

namespace Domain.Factories;

public interface IWorkflowFactory
{
    Workflow Create(string name, Guid projectId, bool isDefault = false);
}