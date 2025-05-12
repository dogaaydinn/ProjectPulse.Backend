using Domain.Modules.Tasks.Entities;
using Domain.Modules.Tasks.Repositories;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Tasks;

public class CommentRepository(AppDbContext context) : BaseRepository<Comment>(context), ICommentRepository
{
    public async Task<List<Comment>> GetByTaskIdAsync(Guid taskId) =>
        await Context.Comments.Where(c => c.TaskItemId == taskId).ToListAsync();
}
