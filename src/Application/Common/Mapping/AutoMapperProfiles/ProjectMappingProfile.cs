using Application.DTOs.Project;
using AutoMapper;
using Domain.Modules.Projects.Entities;

namespace Application.Common.Mapping.AutoMapperProfiles;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.Name));
    }
}