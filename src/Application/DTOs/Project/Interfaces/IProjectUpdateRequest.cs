using Application.DTOs.Common;
using Domain.Modules.Projects.Enums;

namespace Application.DTOs.Project.Interfaces;

public interface IProjectUpdateRequest 
{
    LocalizedStringDto Name { get; }
    LocalizedStringDto? Description { get; }
    DateRangeDto Schedule { get; }
    Guid ManagerId { get; }
    string Status { get; }
    string Priority { get; }
}