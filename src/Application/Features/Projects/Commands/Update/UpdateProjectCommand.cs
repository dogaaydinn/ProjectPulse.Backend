using Domain.Modules.Projects.Enums;
using Shared.ValueObjects;

namespace Application.Features.Projects.Commands.Update;

public record UpdateProjectCommand(
    Guid Id,
    LocalizedString Name,
    LocalizedString Description, 
    DateRange Schedule,
    Guid ManagerId,
    ProjectStatus Status,
    ProjectPriority Priority
);