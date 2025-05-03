using Domain.Factories;
using Domain.Repositories;
using Shared.Results;

namespace Application.Features.Projects.Commands.Create;

public class CreateProjectCommandHandler
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectFactory _projectFactory;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectCommandHandler(
        IProjectRepository projectRepository,
        IProjectFactory projectFactory,
        IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _projectFactory = projectFactory;
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