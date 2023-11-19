using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Shared.ControllerBases;

namespace Onicorn.CRMApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunicationTypeController : CustomBaseController
    {
        private readonly ICommunicationTypeService _communicationTypeService;
        public CommunicationTypeController(ICommunicationTypeService communicationTypeService)
        {
            _communicationTypeService = communicationTypeService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCommunicationTypes()
        {
            return CreateActionResultInstance(await _communicationTypeService.GetCommunicationTypesAsync());
        }
    }
}
