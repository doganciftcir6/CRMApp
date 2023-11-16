using AutoMapper;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.CustomerDtos;
using Onicorn.CRMApp.Entities;
using Onicorn.CRMApp.Shared.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Services.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public CustomerService(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CustomResponse<CustomerDto>> GetCustomerAsync(int customerId)
        {
            Customer customer = await _uow.GetRepository<Customer>().GetByIdAsync(customerId);
            if(customer != null)
            {
                CustomerDto customerDto = _mapper.Map<CustomerDto>(customer);
                return CustomResponse<CustomerDto>.Success(customerDto, ResponseStatusCode.OK);
            }
            return CustomResponse<CustomerDto>.Fail("Customer not found", ResponseStatusCode.NOT_FOUND);
        }

        public async Task<CustomResponse<IEnumerable<CustomersDto>>> GetCustomersAsync()
        {
            var customerDto = _mapper.Map<IEnumerable<CustomersDto>>(await _uow.GetRepository<Customer>().GetAllFilterAsync(x => x.Status == true));
            return CustomResponse<IEnumerable<CustomersDto>>.Success(customerDto, ResponseStatusCode.OK);
        }
    }
}
