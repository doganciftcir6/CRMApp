using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Shared.ControllerBases;
using System.Data;

namespace Onicorn.CRMApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunicationController : CustomBaseController
    {
        private readonly ICommunicationService _communicationService;
        public CommunicationController(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommunications()
        {
            return CreateActionResultInstance(await _communicationService.GetCommunicatios());
        }
    }
}
