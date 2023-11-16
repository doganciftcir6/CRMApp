using AutoMapper;
using Onicorn.CRMApp.Dtos.ProjectDtos;
using Onicorn.CRMApp.Entities;

namespace Onicorn.CRMApp.Business.Mapping.AutoMapper
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectsDto>().ReverseMap();
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Project, ProjectCreateDto>().ReverseMap();
            CreateMap<Project, ProjectUpdateDto>().ReverseMap();
        }
    }
}
