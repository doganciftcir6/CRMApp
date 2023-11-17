using Onicorn.CRMApp.Dtos.SaleDtos;
using Onicorn.CRMApp.Shared.Utilities.Response;

namespace Onicorn.CRMApp.Business.Services.Interfaces
{
    public interface ISaleService
    {
        Task<CustomResponse<IEnumerable<SalesDto>>> GetSalesAsync();
        Task<CustomResponse<NoContent>> InsertSaleAsync(SaleCreateDto saleCreateDto);
        Task<CustomResponse<NoContent>> UpdateSaleAsync(SaleUpdateDto saleUpdateDto);
    }
}
