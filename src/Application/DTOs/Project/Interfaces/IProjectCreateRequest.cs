using Application.DTOs.Common;

namespace Application.DTOs.Project.Interfaces;

public interface IProjectCreateRequest
{
    LocalizedStringDto Name { get; }
    LocalizedStringDto? Description { get; }
    DateRangeDto Schedule { get; }
    Guid ManagerId { get; }
    string Status { get; }
    string Priority { get; }
}