using Domain.Modules.Projects.Entities;
using Domain.Modules.Projects.Repositories;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Projects;

public class WorkflowRepository(AppDbContext context) : BaseRepository<Workflow>(context), IWorkflowRepository
{
    public async Task<List<Workflow>> GetByProjectIdAsync(Guid projectId)
    {
        return await Context.Workflows
            .Where(w => w.ProjectId == projectId)
            .ToListAsync();
    }
}