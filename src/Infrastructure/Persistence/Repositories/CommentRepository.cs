using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context) => _context = context;

    public async Task<Comment?> GetByIdAsync(Guid id) => await _context.Comments.FindAsync(id);
    public async Task<List<Comment>> GetAllAsync() => await _context.Comments.ToListAsync();
    public async Task AddAsync(Comment entity) => await _context.Comments.AddAsync(entity);
    public void Delete(Comment entity) => _context.Comments.Remove(entity);
    public async Task<List<Comment>> GetByTaskIdAsync(Guid taskId) =>
        await _context.Comments.Where(c => c.TaskItemId == taskId).ToListAsync();
}
