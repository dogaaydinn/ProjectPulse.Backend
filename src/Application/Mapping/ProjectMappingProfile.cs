using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        // Entity → DTO
        CreateMap<Project, ProjectDto>();

        // CreateRequest → Entity
        CreateMap<CreateProjectRequest, Project>();

        // UpdateRequest → Entity
        CreateMap<UpdateProjectRequest, Project>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // Genelde update'de ID dışarıdan gelir ve override edilmez
    }
}