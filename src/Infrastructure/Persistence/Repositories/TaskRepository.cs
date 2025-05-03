using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class TaskRepository(AppDbContext context) : BaseRepository<TaskItem>(context), ITaskRepository;