using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class MilestoneRepository(AppDbContext context) : BaseRepository<Milestone>(context), IMilestoneRepository;