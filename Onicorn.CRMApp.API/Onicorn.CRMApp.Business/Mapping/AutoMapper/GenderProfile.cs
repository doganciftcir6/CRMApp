using AutoMapper;
using Onicorn.CRMApp.Dtos.AppUserDtos;
using Onicorn.CRMApp.Dtos.GenderDtos;
using Onicorn.CRMApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Mapping.AutoMapper
{
    public class GenderProfile : Profile
    {
        public GenderProfile()
        {
            CreateMap<Gender, GenderDto>().ReverseMap();
        }
    }
}
