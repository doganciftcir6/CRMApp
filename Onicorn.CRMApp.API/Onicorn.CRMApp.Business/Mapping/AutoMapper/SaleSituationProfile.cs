using AutoMapper;
using Onicorn.CRMApp.Dtos.SaleSituationDtos;
using Onicorn.CRMApp.Entities;

namespace Onicorn.CRMApp.Business.Mapping.AutoMapper
{
    public class SaleSituationProfile : Profile
    {
        public SaleSituationProfile()
        {
            CreateMap<SaleSituation, SaleSituationsDto>().ReverseMap();
        }
    }
}
