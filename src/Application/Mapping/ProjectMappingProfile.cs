using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<Project, ProjectDto>();
        
    }
}