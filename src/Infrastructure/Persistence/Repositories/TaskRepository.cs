using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context) => _context = context;

    public async Task<TaskItem?> GetByIdAsync(Guid id) => await _context.Tasks.FindAsync(id);
    public async Task<List<TaskItem>> GetAllAsync() => await _context.Tasks.ToListAsync();
    public async Task AddAsync(TaskItem entity) => await _context.Tasks.AddAsync(entity);
    public void Delete(TaskItem entity) => _context.Tasks.Remove(entity);
}