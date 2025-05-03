using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class StatusRepository : IStatusRepository
{
    private readonly AppDbContext _context;

    public StatusRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Status?> GetByIdAsync(Guid id)
    {
        return await _context.Statuses
            .Include(s => s.Workflow)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Status>> GetAllAsync()
    {
        return await _context.Statuses.ToListAsync();
    }

    public async Task AddAsync(Status entity)
    {
        await _context.Statuses.AddAsync(entity);
    }

    public void Delete(Status entity)
    {
        _context.Statuses.Remove(entity);
    }

    public async Task<List<Status>> GetByWorkflowIdAsync(Guid workflowId)
    {
        return await _context.Statuses
            .Where(s => s.WorkflowId == workflowId)
            .OrderBy(s => s.Order)
            .ToListAsync();
    }
}