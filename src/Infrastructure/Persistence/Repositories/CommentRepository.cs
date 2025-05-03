using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context) { }

    public async Task<List<Comment>> GetByTaskIdAsync(Guid taskId) =>
        await _context.Comments.Where(c => c.TaskItemId == taskId).ToListAsync();
}
