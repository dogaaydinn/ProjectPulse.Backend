using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class WorkflowRepository : BaseRepository<Workflow>, IWorkflowRepository
{
    public WorkflowRepository(AppDbContext context) : base(context) { }

    public async Task<List<Workflow>> GetByProjectIdAsync(Guid projectId)
    {
        return await _context.Workflows
            .Where(w => w.ProjectId == projectId)
            .ToListAsync();
    }
}