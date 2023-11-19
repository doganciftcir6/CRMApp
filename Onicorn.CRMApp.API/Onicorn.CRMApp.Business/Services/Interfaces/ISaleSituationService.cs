using Onicorn.CRMApp.Dtos.SaleSituationDtos;
using Onicorn.CRMApp.Shared.Utilities.Response;

namespace Onicorn.CRMApp.Business.Services.Interfaces
{
    public interface ISaleSituationService
    {
        Task<CustomResponse<IEnumerable<SaleSituationsDto>>> GetSaleSituationsAsync();
    }
}
