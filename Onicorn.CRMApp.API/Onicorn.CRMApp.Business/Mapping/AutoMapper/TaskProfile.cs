using AutoMapper;
using Onicorn.CRMApp.Dtos.TaskDtos;
using Task = Onicorn.CRMApp.Entities.Task;

namespace Onicorn.CRMApp.Business.Mapping.AutoMapper
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TasksDto>().ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src.AppUser != null ? src.AppUser.Firstname + src.AppUser.Lastname : null)).ForMember(dest => dest.TaskSituation, opt => opt.MapFrom(src => src.TaskSituation != null ? src.TaskSituation.Definition : null)).ReverseMap();
        }
    }
}
