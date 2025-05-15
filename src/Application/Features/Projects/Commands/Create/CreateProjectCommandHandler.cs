using Application.Common.Handlers;
using Application.Common.Validation;
using Application.Interfaces;
using Domain.Core.Persistence;
using Domain.Factories;
using Domain.Modules.Projects.Repositories;
using Shared.Results;

namespace Application.Features.Projects.Commands.Create;

public class CreateProjectCommandHandler 
    : BaseCommandHandler<CreateProjectCommand, Guid>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectFactory _projectFactory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    
    public CreateProjectCommandHandler(
        IProjectRepository projectRepository,
        IProjectFactory projectFactory,
        IUnitOfWork unitOfWork,
        IValidator<CreateProjectCommand> validator,
        ICurrentUserService currentUserService)
        : base(validator)
    {
        _projectRepository = projectRepository;
        _projectFactory = projectFactory;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Guid>> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        return await ValidateAndExecuteAsync(command, async () =>
        {
            var currentUserId = _currentUserService.UserId;
            if (currentUserId == Guid.Empty)
                return Result<Guid>.Failure(ErrorFactory.Required("CurrentUser"));

            var model = new CreateProjectModel(command,currentUserId);
            var project = _projectFactory.Create(model);

            await _projectRepository.AddAsync(project);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(project.Id);
        });
    }
}
/*
 Ekstra İyileştirme (ileride yapılabilir):
   ProjectFactory.Create(...) parametreleri CreateProjectCommand alanlarını çok fazla tekrar ediyor. DTO to Domain Builder/Fatory uyumlu hale getirilebilir.
   
   */