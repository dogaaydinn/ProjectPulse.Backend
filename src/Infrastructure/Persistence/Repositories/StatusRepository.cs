using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class StatusRepository : BaseRepository<Status>, IStatusRepository
{
    public StatusRepository(AppDbContext context) : base(context) { }
    
    public async Task<List<Status>> GetByWorkflowIdAsync(Guid workflowId)
    {
        return await _context.Statuses
            .Where(s => s.WorkflowId == workflowId)
            .OrderBy(s => s.Order)
            .ToListAsync();
    }
}