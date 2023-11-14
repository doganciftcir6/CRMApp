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
            if (!result)
            {
                return BadRequest("User eklenemedi");
            }
            return Ok("User eklendi");
        }
        [HttpPost("LoginUser")]
        public async Task<IActionResult> Login(AppUserLoginDto appUserLoginDto)
        {

            var result = await _authService.LoginAsync(appUserLoginDto);
            if (!result)
            {
                return BadRequest("Login başarısız");
            }
            return Ok("Login başarılı");
        }
    }
}
