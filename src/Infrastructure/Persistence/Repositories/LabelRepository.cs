using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class LabelRepository(AppDbContext context) : BaseRepository<Label>(context), ILabelRepository;