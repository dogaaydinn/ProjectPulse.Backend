using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository;