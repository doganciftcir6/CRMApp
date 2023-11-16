using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Dtos.ProjectDtos;
using Onicorn.CRMApp.Shared.ControllerBases;

namespace Onicorn.CRMApp.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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

        [Authorize( Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertProject([FromForm] ProjectCreateDto projectCreateDto, CancellationToken cancellationToken)
        {
            return CreateActionResultInstance(await _projectService.InsertProjectAsync(projectCreateDto, cancellationToken));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProject([FromForm] ProjectUpdateDto projectUpdateDto, CancellationToken cancellationToken)
        {
            return CreateActionResultInstance(await _projectService.UpdateProjectAsync(projectUpdateDto, cancellationToken));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]/{projectId}")]
        public async Task<IActionResult> RemoveProject(int projectId)
        {
            return CreateActionResultInstance(await _projectService.RemoveProjectAsync(projectId));
        }
    }
}
