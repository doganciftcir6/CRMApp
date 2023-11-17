using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Dtos.CustomerDtos;
using Onicorn.CRMApp.Shared.ControllerBases;

namespace Onicorn.CRMApp.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CustomerController : CustomBaseController
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomers()
        {
            return CreateActionResultInstance(await _customerService.GetCustomersAsync());
        }

        [HttpGet("[action]/{customerId}")]
        public async Task<IActionResult> GetCustomer(int customerId)
        {
            return CreateActionResultInstance(await _customerService.GetCustomerByIdAsync(customerId));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertCustomer(CustomerCreateDto customerCreateDto)
        {
            return CreateActionResultInstance(await _customerService.InsertCustomerAsync(customerCreateDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateCustomer(CustomerUpdateDto customerUpdateDto)
        {
            return CreateActionResultInstance(await _customerService.UpdateCustomerAsync(customerUpdateDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]/{customerId}")]
        public async Task<IActionResult> RemoveCustomer(int customerId)
        {
            return CreateActionResultInstance(await _customerService.RemoveCustomerAsync(customerId));
        }
    }
}
