using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class LabelRepository : BaseRepository<Label>, ILabelRepository
{
    public LabelRepository(AppDbContext context) : base(context) { }
}