using Domain.Factories;
using Domain.Modules.Projects.Entities;

namespace Infrastructure.Factories;

public class WorkflowFactory : IWorkflowFactory
{
    public Workflow Create(string name, Guid projectId, bool isDefault = false)
    {
        return new Workflow(name, projectId, isDefault);
    }
}