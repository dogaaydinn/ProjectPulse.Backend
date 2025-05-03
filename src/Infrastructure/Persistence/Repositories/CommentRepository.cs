using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CommentRepository(AppDbContext context) : BaseRepository<Comment>(context), ICommentRepository
{
    public async Task<List<Comment>> GetByTaskIdAsync(Guid taskId) =>
        await Context.Comments.Where(c => c.TaskItemId == taskId).ToListAsync();
}
