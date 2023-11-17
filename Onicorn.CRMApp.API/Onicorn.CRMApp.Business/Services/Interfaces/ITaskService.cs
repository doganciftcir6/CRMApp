using Onicorn.CRMApp.Dtos.TaskDtos;
using Onicorn.CRMApp.Shared.Utilities.Response;

namespace Onicorn.CRMApp.Business.Services.Interfaces
{
    public interface ITaskService
    {
        Task<CustomResponse<IEnumerable<TasksDto>>> GetTasks();
    }
}
