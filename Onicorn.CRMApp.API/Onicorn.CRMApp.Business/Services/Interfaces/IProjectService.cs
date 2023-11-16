using Onicorn.CRMApp.Dtos.ProjectDtos;
using Onicorn.CRMApp.Shared.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Services.Interfaces
{
    public interface IProjectService
    {
        Task<CustomResponse<IEnumerable<ProjectsDto>>> GetProjectsAsync();
        Task<CustomResponse<ProjectDto>> GetProjectAsync(int projectId);
    }
}
