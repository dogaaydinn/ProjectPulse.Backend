using Domain.Entities;

namespace Domain.Repositories;

public interface IStatusRepository : IRepository<Status>
{
    Task<List<Status>> GetByWorkflowIdAsync(Guid workflowId);
}