using Domain.Modules.Users.Entities;
using Domain.Modules.Users.Repositories;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories.Users;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository;