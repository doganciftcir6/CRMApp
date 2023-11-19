using AutoMapper;
using Onicorn.CRMApp.Dtos.CommunicationTypeDtos;
using Onicorn.CRMApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Mapping.AutoMapper
{
    public class CommunicationTypeProfile : Profile
    {
        public CommunicationTypeProfile()
        {
            CreateMap<CommunicationType, CommunicationTypesDto>().ReverseMap();
        }
    }
}
