using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class WorkflowRepository(AppDbContext context) : BaseRepository<Workflow>(context), IWorkflowRepository
{
    public async Task<List<Workflow>> GetByProjectIdAsync(Guid projectId)
    {
        return await Context.Workflows
            .Where(w => w.ProjectId == projectId)
            .ToListAsync();
    }
}