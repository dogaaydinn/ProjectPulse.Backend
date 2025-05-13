using Application.Common.Handlers;
using Application.Common.Validation;
using Domain.Core.Persistence;
using Domain.Modules.Projects.Repositories;
using Shared.Results;

namespace Application.Features.Projects.Commands.Delete;

public class DeleteProjectCommandHandler
    : BaseCommandHandler<DeleteProjectCommand, Result>
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

    public async Task<Result> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
    {
        return await ValidateAndExecuteAsync(command, async () =>
        {
            var project = await _projectRepository.GetByIdAsync(command.Id);
            if (project is null)
                return Result.Failure(Error.NotFound("Project", command.Id));

            _projectRepository.Delete(project);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        });
    }
}