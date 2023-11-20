using AutoMapper;
using Onicorn.CRMApp.Dtos.TaskSituationDtos;
using Onicorn.CRMApp.Entities;

namespace Onicorn.CRMApp.Business.Mapping.AutoMapper
{
    public class TaskSituationProfile : Profile
    {
        public TaskSituationProfile()
        {
            CreateMap<TaskSituation, TaskSituationsDto>().ReverseMap();
        }
    }
}
