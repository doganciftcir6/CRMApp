using Onicorn.CRMApp.Dtos.TaskSituationDtos;
using Onicorn.CRMApp.Shared.Utilities.Response;

namespace Onicorn.CRMApp.Business.Services.Interfaces
{
    public interface ITaskSituationService
    {
        Task<CustomResponse<IEnumerable<TaskSituationsDto>>> GetTaskSituationsAsync();
    }
}
