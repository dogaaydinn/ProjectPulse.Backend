using Domain.Modules.Projects.Entities;
using Domain.Modules.Projects.Repositories;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Projects;

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