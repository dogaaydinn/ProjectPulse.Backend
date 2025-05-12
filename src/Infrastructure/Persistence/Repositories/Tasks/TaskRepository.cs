using Domain.Modules.Tasks.Entities;
using Domain.Modules.Tasks.Repositories;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories.Tasks;

public class TaskRepository(AppDbContext context) : BaseRepository<TaskItem>(context), ITaskRepository
{
    public async Task AddAsync(TaskItem taskItem, CancellationToken cancellationToken)
    {
        await Context.Set<TaskItem>().AddAsync(taskItem, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TaskItem taskItem, CancellationToken cancellationToken)
    {
        Context.Set<TaskItem>().Remove(taskItem);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.Set<TaskItem>().FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task UpdateAsync(TaskItem taskItem, CancellationToken cancellationToken)
    {
        Context.Set<TaskItem>().Update(taskItem);
        await Context.SaveChangesAsync(cancellationToken);
    }
}