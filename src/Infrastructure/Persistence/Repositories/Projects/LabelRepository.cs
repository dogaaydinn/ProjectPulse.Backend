using Domain.Modules.Projects.Entities;
using Domain.Modules.Projects.Repositories;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories.Projects;

public class LabelRepository(AppDbContext context) : BaseRepository<Label>(context), ILabelRepository;