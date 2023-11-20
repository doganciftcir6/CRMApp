using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Shared.ControllerBases;

namespace Onicorn.CRMApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskSituationController : CustomBaseController
    {
        private readonly ITaskSituationService _taskSituationService;
        public TaskSituationController(ITaskSituationService taskSituationService)
        {
            _taskSituationService = taskSituationService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTaskSituations()
        {
            return CreateActionResultInstance(await _taskSituationService.GetTaskSituationsAsync());
        }
    }
}
