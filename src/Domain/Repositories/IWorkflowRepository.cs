using Domain.Entities;

namespace Domain.Repositories;

public interface IWorkflowRepository : IRepository<Workflow>
{
    public interface IWorkflowRepository
    {
        Task<Workflow?> GetByIdAsync(Guid id);
        Task<List<Workflow>> GetByProjectIdAsync(Guid projectId); // Bunu ekle
        Task AddAsync(Workflow workflow);
    }

}