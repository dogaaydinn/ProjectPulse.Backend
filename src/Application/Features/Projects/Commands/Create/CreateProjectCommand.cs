using Domain.Modules.Projects.Enums;
using Shared.ValueObjects;

namespace Application.Features.Projects.Commands.Create;

public record CreateProjectCommand(
    LocalizedString Name,
    LocalizedString Description,
    DateRange Schedule,
    Guid ManagerId,
    ProjectStatus Status,
    ProjectPriority Priority);

