using Domain.Core.Persistence;
using Domain.Modules.Projects.Entities;

namespace Domain.Modules.Projects.Repositories;

public interface IStatusRepository : IRepository<Status>
{
    Task<List<Status>> GetByWorkflowIdAsync(Guid workflowId);
}