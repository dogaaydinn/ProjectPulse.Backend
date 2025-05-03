using Domain.Entities;

namespace Domain.Repositories;

public interface IWorkflowRepository : IRepository<Workflow>
{

        Task<List<Workflow>> GetByProjectIdAsync(Guid projectId); 
        

}