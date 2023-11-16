using Onicorn.CRMApp.Dtos.CustomerDtos;
using Onicorn.CRMApp.Shared.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomResponse<IEnumerable<CustomersDto>>> GetCustomersAsync();
        Task<CustomResponse<CustomerDto>> GetCustomerAsync(int customerId);
    }
}
