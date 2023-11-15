using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Dtos.AppUserDtos;
using Onicorn.CRMApp.Shared.ControllerBases;

namespace Onicorn.CRMApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppUserController : CustomBaseController
    {
        private readonly IAppUserService _appUserService;
        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProfile()
        {
            return CreateActionResultInstance(await _appUserService.GetProfileAsync());
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProfile(UpdateAppUserDto updateAppUserDto)
        {
            return CreateActionResultInstance(await _appUserService.UpdateProfileAsync(updateAppUserDto));
        }
    }
}
