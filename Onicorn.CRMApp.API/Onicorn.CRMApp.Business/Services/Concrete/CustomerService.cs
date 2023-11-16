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

        public async Task<CustomResponse<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customerDto = _mapper.Map<IEnumerable<CustomerDto>>(await _uow.GetRepository<Customer>().GetAllFilterAsync(x => x.Status == true));
            return CustomResponse<IEnumerable<CustomerDto>>.Success(customerDto, ResponseStatusCode.OK);
        }
    }
}
