using AutoMapper;
using Onicorn.CRMApp.Dtos.CustomerDtos;
using Onicorn.CRMApp.Entities;

namespace Onicorn.CRMApp.Business.Mapping.AutoMapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomersDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerCreateDto>().ReverseMap();
            CreateMap<Customer, CustomerUpdateDto>().ReverseMap();
        }
    }
}
