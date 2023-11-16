using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Dtos.CommunicationDtos;
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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCommunications()
        {
            return CreateActionResultInstance(await _communicationService.GetCommunicatiosAsync());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertCommunication(CommunicationCreateDto communicationCreateDto)
        {
            return CreateActionResultInstance(await _communicationService.InsertCommunicationAsync(communicationCreateDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateCommunication(CommunicationUpdateDto communicationUpdateDto)
        {
            return CreateActionResultInstance(await _communicationService.UpdateCommunicationAsync(communicationUpdateDto));
        }
    }
}
