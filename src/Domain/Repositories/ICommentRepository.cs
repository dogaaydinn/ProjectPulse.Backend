using Domain.Entities;

namespace Domain.Repositories;

public interface ICommentRepository : IRepository<Comment>  
{
    Task<List<Comment>> GetByTaskIdAsync(Guid taskId);
}