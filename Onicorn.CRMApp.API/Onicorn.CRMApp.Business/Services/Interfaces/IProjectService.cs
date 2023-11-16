using Onicorn.CRMApp.Dtos.ProjectDtos;
using Onicorn.CRMApp.Shared.Utilities.Response;

namespace Onicorn.CRMApp.Business.Services.Interfaces
{
    public interface IProjectService
    {
        Task<CustomResponse<IEnumerable<ProjectsDto>>> GetProjectsAsync();
        Task<CustomResponse<ProjectDto>> GetProjectAsync(int projectId);
        Task<CustomResponse<NoContent>> InsertProjectAsync(ProjectCreateDto projectCreateDto, CancellationToken cancellationToken);
        Task<CustomResponse<NoContent>> UpdateProjectAsync(ProjectUpdateDto projectUpdateDto, CancellationToken cancellationToken);
        Task<CustomResponse<NoContent>> RemoveProjectAsync(int projectId);
    }
}
