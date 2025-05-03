using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class MilestoneRepository : IMilestoneRepository
{
    private readonly AppDbContext _context;

    public MilestoneRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Milestone?> GetByIdAsync(Guid id)
    {
        return await _context.Milestones
            .Include(m => m.Project)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<Milestone>> GetAllAsync()
    {
        return await _context.Milestones.ToListAsync();
    }

    public async Task AddAsync(Milestone entity)
    {
        await _context.Milestones.AddAsync(entity);
    }

    public void Delete(Milestone entity)
    {
        _context.Milestones.Remove(entity);
    }
}