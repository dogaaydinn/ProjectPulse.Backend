using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _context;

    public ProjectRepository(AppDbContext context) => _context = context;
    
    public async Task<Project?> GetByIdAsync(Guid id) => await _context.Projects.FindAsync(id);
    public async Task<List<Project>> GetAllAsync() => await _context.Projects.ToListAsync();
    public async Task AddAsync(Project entity) => await _context.Projects.AddAsync(entity);
    public void Delete(Project entity) => _context.Projects.Remove(entity);
}