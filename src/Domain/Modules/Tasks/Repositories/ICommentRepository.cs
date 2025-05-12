using Domain.Core.Persistence;
using Domain.Modules.Tasks.Entities;

namespace Domain.Modules.Tasks.Repositories;

public interface ICommentRepository : IRepository<Comment>  
{
    Task<List<Comment>> GetByTaskIdAsync(Guid taskId);
}