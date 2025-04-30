using Application.DTOs;
using AutoMapper;

namespace Application.Mapping;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<CreateProjectRequest, ProjectDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}