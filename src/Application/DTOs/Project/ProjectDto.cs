using Shared.ValueObjects;

namespace Application.DTOs.Project;

public record ProjectDto(
    Guid Id,
    LocalizedString Name,
    LocalizedString Description,
    DateRange Schedule,
    Guid ManagerId,
    string Status,
    string Priority
);