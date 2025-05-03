using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class TaskMappingProfile : Profile
{
    public TaskMappingProfile()
    {
        CreateMap<TaskItem, TaskDto>();

        CreateMap<CreateTaskRequest, TaskItem>();
        CreateMap<UpdateTaskRequest, TaskItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}