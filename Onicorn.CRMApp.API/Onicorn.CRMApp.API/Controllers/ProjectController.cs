using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Shared.ControllerBases;

namespace Onicorn.CRMApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : CustomBaseController
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProjects()
        {
            return CreateActionResultInstance(await _projectService.GetProjectsAsync());
        }

        [HttpGet("[action]/{projectId}")]
        public async Task<IActionResult> GetProject(int projectId)
        {
            return CreateActionResultInstance(await _projectService.GetProjectAsync(projectId));
        }
    }
}
