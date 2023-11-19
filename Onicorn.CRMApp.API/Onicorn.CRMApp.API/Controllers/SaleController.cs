using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Dtos.SaleDtos;
using Onicorn.CRMApp.Shared.ControllerBases;

namespace Onicorn.CRMApp.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SaleController : CustomBaseController
    {
        private readonly ISaleService _saleService;
        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSales()
        {
            return CreateActionResultInstance(await _saleService.GetSalesAsync());
        }

        [HttpGet("[action]/{saleId}")]
        public async Task<IActionResult> GetSaleById(int saleId)
        {
            return CreateActionResultInstance(await _saleService.GetSaleByIdAsync(saleId));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertSale(SaleCreateDto saleCreateDto)
        {
            return CreateActionResultInstance(await _saleService.InsertSaleAsync(saleCreateDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateSale(SaleUpdateDto saleUpdateDto)
        {
            return CreateActionResultInstance(await _saleService.UpdateSaleAsync(saleUpdateDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]/{saleId}")]
        public async Task<IActionResult> RemoveSale(int saleId)
        {
            return CreateActionResultInstance(await _saleService.RemoveSaleAsync(saleId));
        }
    }
}
