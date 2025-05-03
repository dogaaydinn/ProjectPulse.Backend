using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class MilestoneRepository : BaseRepository<Milestone>, IMilestoneRepository
{
    public MilestoneRepository(AppDbContext context) : base(context) { }
}