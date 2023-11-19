using Onicorn.CRMApp.Dtos.SaleDtos;
using Onicorn.CRMApp.Shared.Utilities.Response;

namespace Onicorn.CRMApp.Business.Services.Interfaces
{
    public interface ISaleService
    {
        Task<CustomResponse<IEnumerable<SalesDto>>> GetSalesAsync();
        Task<CustomResponse<SalesDto>> GetSaleByIdAsync(int saleId);
        Task<CustomResponse<NoContent>> InsertSaleAsync(SaleCreateDto saleCreateDto);
        Task<CustomResponse<NoContent>> UpdateSaleAsync(SaleUpdateDto saleUpdateDto);
        Task<CustomResponse<NoContent>> RemoveSaleAsync(int saleId);
    }
}
