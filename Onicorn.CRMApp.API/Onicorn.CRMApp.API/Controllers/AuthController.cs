using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Dtos.AppUserDtos;

namespace Onicorn.CRMApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Register(AppUserRegisterDto appUserRegisterDto)
        {
            var result = await _authService.RegisterWithRoleAsync(appUserRegisterDto);
            return Ok();
        }
        [HttpPost("LoginUser")]
        public async Task<IActionResult> Login(AppUserLoginDto appUserLoginDto)
        {
            var result = await _authService.LoginAsync(appUserLoginDto);
            return Ok("Login başarılı");
        }
    }
}
