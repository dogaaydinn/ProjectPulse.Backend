using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class StatusRepository(AppDbContext context) : BaseRepository<Status>(context), IStatusRepository
{
    public async Task<List<Status>> GetByWorkflowIdAsync(Guid workflowId)
    {
        return await Context.Statuses
            .Where(s => s.WorkflowId == workflowId)
            .OrderBy(s => s.Order)
            .ToListAsync();
    }
}