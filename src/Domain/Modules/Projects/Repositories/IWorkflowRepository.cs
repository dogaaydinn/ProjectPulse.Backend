using Domain.Core.Persistence;
using Domain.Modules.Projects.Entities;

namespace Domain.Modules.Projects.Repositories;

public interface IWorkflowRepository : IRepository<Workflow>
{

        Task<List<Workflow>> GetByProjectIdAsync(Guid projectId); 
        

}