using Domain.Factories;
using Shared.Results;

namespace Application.Features.Projects.Commands.Create;

public class CreateProjectCommandHandler
{
    private readonly IProjectFactory _projectFactory;
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectCommandHandler(
        IProjectFactory projectFactory,
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork)
    {
        _projectFactory = projectFactory;
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = _projectFactory.Create(
            command.Name,
            command.Description,
            command.StartDate,
            command.EndDate,
            command.ManagerId);

        await _projectRepository.AddAsync(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(project.Id);
    }
}