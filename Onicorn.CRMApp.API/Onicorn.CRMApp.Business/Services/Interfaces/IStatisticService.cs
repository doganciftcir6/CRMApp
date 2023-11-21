using Onicorn.CRMApp.Dtos.StatisticDtos;
using Onicorn.CRMApp.Shared.Utilities.Response;

namespace Onicorn.CRMApp.Business.Services.Interfaces
{
    public interface IStatisticService
    {
        Task<CustomResponse<StatisticDto>> GetStatisticsAsync();
    }
}
