using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Shared.ControllerBases;

namespace Onicorn.CRMApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : CustomBaseController
    {
        private readonly IGenderService _genderService;
        public GenderController(IGenderService genderService)
        {
            _genderService = genderService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetGenders()
        {
            return CreateActionResultInstance(await _genderService.GetGendersAsync());
        }
    }
}
