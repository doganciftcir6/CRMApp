using Onicorn.CRMApp.Dtos.SaleDtos;
using Onicorn.CRMApp.Shared.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Services.Interfaces
{
    public interface ISaleService
    {
        Task<CustomResponse<IEnumerable<SalesDto>>> GetSalesAsync();
        Task<CustomResponse<NoContent>> InsertSaleAsync(SaleCreateDto saleCreateDto);
    }
}
