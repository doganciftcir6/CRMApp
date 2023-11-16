using AutoMapper;
using Onicorn.CRMApp.Dtos.AppUserDtos;
using Onicorn.CRMApp.Dtos.CommunicationDtos;
using Onicorn.CRMApp.Entities;

namespace Onicorn.CRMApp.Business.Mapping.AutoMapper
{
    public class CommunicationProfile : Profile
    {
        public CommunicationProfile()
        {
            CreateMap<Communication, CommunicationDto>().ForMember(dest => dest.CommunicationType, opt => opt.MapFrom(src => src.CommunicationType != null ? src.CommunicationType.Definition : null)).ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.CompanyName : null)).ReverseMap();
            CreateMap<Communication, CommunicationCreateDto>().ReverseMap();
            CreateMap<Communication, CommunicationUpdateDto>().ReverseMap();
        }
    }
}
