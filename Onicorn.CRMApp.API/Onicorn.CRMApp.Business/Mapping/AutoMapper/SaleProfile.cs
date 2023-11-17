using AutoMapper;
using Onicorn.CRMApp.Dtos.SaleDtos;
using Onicorn.CRMApp.Entities;

namespace Onicorn.CRMApp.Business.Mapping.AutoMapper
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<Sale, SalesDto>().ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.CompanyName : null)).ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project != null ? src.Project.ProjectName : null)).ForMember(dest => dest.SaleSituation, opt => opt.MapFrom(src => src.SaleSituation != null ? src.SaleSituation.Definition : null)).ReverseMap();
            CreateMap<Sale, SaleCreateDto>().ReverseMap();
            CreateMap<Sale, SaleUpdateDto>().ReverseMap();
        }
    }
}
