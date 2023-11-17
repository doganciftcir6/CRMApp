using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Dtos.TaskDtos;
using Onicorn.CRMApp.Shared.ControllerBases;

namespace Onicorn.CRMApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : CustomBaseController
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTasks()
        {
            return CreateActionResultInstance(await _taskService.GetTasksAsync());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTasksByUser()
        {
            return CreateActionResultInstance(await _taskService.GetTasksByUserAsync());
        }

        [HttpGet("[action]/{taskId}")]
        public async Task<IActionResult> GetTask(int taskId)
        {
            return CreateActionResultInstance(await _taskService.GetTaskAsync(taskId));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InsertTask(TaskCreateDto taskCreateDto)
        {
            return CreateActionResultInstance(await _taskService.InsertTaskAsync(taskCreateDto));
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateTask(TaskUpdateDto taskUpdateDto)
        {
            return CreateActionResultInstance(await _taskService.UpdateTaskAsync(taskUpdateDto));
        }
    }
}
