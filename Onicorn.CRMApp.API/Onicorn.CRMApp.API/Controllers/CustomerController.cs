using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Dtos.CustomerDtos;
using Onicorn.CRMApp.Shared.ControllerBases;

namespace Onicorn.CRMApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : CustomBaseController
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomers()
        {
            return CreateActionResultInstance(await _customerService.GetCustomersAsync());
        }

        [Authorize]
        [HttpGet("[action]/{customerId}")]
        public async Task<IActionResult> GetCustomer(int customerId)
        {
            return CreateActionResultInstance(await _customerService.GetCustomerAsync(customerId));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertCustomer(CustomerCreateDto customerCreateDto)
        {
            return CreateActionResultInstance(await _customerService.InsertCustomerAsync(customerCreateDto));
        }
    }
}
