using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Dtos.AppUserDtos;
using Onicorn.CRMApp.Shared.ControllerBases;

namespace Onicorn.CRMApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Register([FromForm]AppUserRegisterDto appUserRegisterDto, CancellationToken cancellationToken)
        {
            return CreateActionResultInstance(await _authService.RegisterWithRoleAsync(appUserRegisterDto, cancellationToken));
            ;
        }
        [HttpPost("LoginUser")]
        public async Task<IActionResult> Login(AppUserLoginDto appUserLoginDto)
        {
            return CreateActionResultInstance(await _authService.LoginAsync(appUserLoginDto));

        }
    }
}
