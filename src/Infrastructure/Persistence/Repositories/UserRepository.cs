using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) => _context = context;

    public async Task<User?> GetByIdAsync(Guid id) => await _context.Users.FindAsync(id);
    public async Task<List<User>> GetAllAsync() => await _context.Users.ToListAsync();
    public async Task AddAsync(User entity) => await _context.Users.AddAsync(entity);
    public void Delete(User entity) => _context.Users.Remove(entity);
}