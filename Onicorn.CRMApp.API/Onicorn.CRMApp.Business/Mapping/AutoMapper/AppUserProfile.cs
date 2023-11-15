using AutoMapper;
using Onicorn.CRMApp.Dtos.AppUserDtos;
using Onicorn.CRMApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Mapping.AutoMapper
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, AppUserRegisterDto>().ReverseMap();
            CreateMap<AppUser, AppUserLoginDto>().ReverseMap();
            CreateMap<AppUser, AppUserDto>().ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender != null ? src.Gender.Definition : null)).ReverseMap();
        }
    }
}
