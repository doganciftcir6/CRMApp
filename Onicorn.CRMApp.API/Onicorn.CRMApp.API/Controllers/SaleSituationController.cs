using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Shared.ControllerBases;

namespace Onicorn.CRMApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleSituationController : CustomBaseController
    {
        private readonly ISaleSituationService _saleSituationService;
        public SaleSituationController(ISaleSituationService saleSituationService)
        {
            _saleSituationService = saleSituationService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSaleSituations()
        {
            return CreateActionResultInstance(await _saleSituationService.GetSaleSituationsAsync());
        }
    }
}
