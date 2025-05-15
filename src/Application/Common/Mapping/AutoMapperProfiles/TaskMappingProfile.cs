using Application.DTOs.Task;
using AutoMapper;
using Domain.Modules.Tasks.Entities;

namespace Application.Common.Mapping.AutoMapperProfiles;

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