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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAppUsers()
        {
            return CreateActionResultInstance(await _appUserService.GetAppUsersAsync());
        }

        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetRoles(int userId)
        {
            return CreateActionResultInstance(await _appUserService.GetRolesAsync(userId.ToString()));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> AssingRole(RoleAssingSendDto roleAssingSendDto)
        {
            return CreateActionResultInstance(await _appUserService.AssingRoleAsync(roleAssingSendDto));
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateAppUserDto updateAppUserDto, CancellationToken cancellationToken)
        {
            return CreateActionResultInstance(await _appUserService.UpdateProfileAsync(updateAppUserDto, cancellationToken));
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> ChangePassword(AppUserChangePasswordDto appUserChangePasswordDto)
        {
            return CreateActionResultInstance(await _appUserService.ChangePasswordAsync(appUserChangePasswordDto));
        }
    }
}
