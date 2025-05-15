using Application.Common.Handlers;
using Application.Common.Validation;
using Domain.Core.Persistence;
using Domain.Modules.Projects.Repositories;
using Shared.Results;
using Shared.Primitives;

namespace Application.Features.Projects.Commands.Delete;

public class DeleteProjectCommandHandler 
    : BaseCommandHandler<DeleteProjectCommand, Unit>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProjectCommandHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork,
        IValidator<DeleteProjectCommand> validator)
        : base(validator)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
    {
        return await ValidateAndExecuteAsync(command, async () =>
        {
            var project = await _projectRepository.GetByIdAsync(command.Id);
            if (project is null)
                return Result<Unit>.Failure(ErrorFactory.NotFound("Project", command.Id));

            _projectRepository.Delete(project);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        });
    }
}