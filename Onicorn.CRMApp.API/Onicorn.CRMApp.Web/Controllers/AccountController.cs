using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Onicorn.CRMApp.Shared.Utilities.Response;
using Onicorn.CRMApp.Web.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace Onicorn.CRMApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Login()
        {
            return View(new AppUserLoginInput());
        }
        [HttpPost]
        public async Task<IActionResult> Login(AppUserLoginInput appUserLoginInput)
        {
            if (ModelState.IsValid)
            {
                var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                var response = await _httpClient.PostAsJsonAsync("Auth/LoginUser", appUserLoginInput);
                if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                    ModelState.AddModelError("", "Authentication server error. Please try again later.");

                CustomResponse<AppUserLoginVM> loginResponse = await response.Content.ReadFromJsonAsync<CustomResponse<AppUserLoginVM>>();
                if (loginResponse.IsSuccessful)
                {
                    //Login
                    if (loginResponse != null)
                    {
                        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                        //Responstan gelen tokeni oku
                        var token = handler.ReadJwtToken(loginResponse.Data.Token);
                        var claims = token.Claims.ToList();
                        if (loginResponse.Data.Token != null)
                            claims.Add(new Claim("accessToken", loginResponse.Data.Token));
                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                        var authProps = new AuthenticationProperties
                        {
                            ExpiresUtc = loginResponse.Data.ExpireDate,
                            IsPersistent = true,
                        };
                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    foreach (var error in loginResponse.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(appUserLoginInput);
        }

        public async Task<IActionResult> Register()
        {
            var model = new AppUserRegisterInput();
            var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
            var response = await _httpClient.GetAsync("Gender/GetGenders");
            if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                ModelState.AddModelError("", "Authentication server error. Please try again later.");
            CustomResponse<IEnumerable<GenderVM>> genderVM = await response.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<GenderVM>>>();

            if (genderVM.IsSuccessful)
            {
                model.Genders = new SelectList(genderVM.Data, "Id", "Definition");
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(AppUserRegisterInput appUserRegisterInput)
        {
            if (ModelState.IsValid)
            {
                var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                var multipartContent = new MultipartFormDataContent();

                if (appUserRegisterInput.ImageURL is not null)
                {
                    using var ms = new MemoryStream();
                    ms.Position = 0;
                    await appUserRegisterInput.ImageURL.CopyToAsync(ms);
                    multipartContent.Add(new ByteArrayContent(ms.ToArray()), "ImageURL", appUserRegisterInput.ImageURL.FileName);
                }
                multipartContent.Add(new StringContent(appUserRegisterInput.Firstname), "Firstname");
                multipartContent.Add(new StringContent(appUserRegisterInput.Lastname), "Lastname");
                multipartContent.Add(new StringContent(appUserRegisterInput.Username), "Username");
                multipartContent.Add(new StringContent(appUserRegisterInput.Email), "Email");
                multipartContent.Add(new StringContent(appUserRegisterInput.Password.ToString()), "Password");
                multipartContent.Add(new StringContent(appUserRegisterInput.ConfirmPassword.ToString()), "ConfirmPassword");
                multipartContent.Add(new StringContent(appUserRegisterInput.PhoneNumber.ToString()), "PhoneNumber");
                multipartContent.Add(new StringContent(appUserRegisterInput.GenderId.ToString()), "GenderId");

                var response = await _httpClient.PostAsync("Auth/RegisterUser", multipartContent);
                if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                    ModelState.AddModelError("", "Authentication server error. Please try again later.");

                CustomResponse<string> registerResponse = await response.Content.ReadFromJsonAsync<CustomResponse<string>>();
                if (registerResponse.IsSuccessful)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in registerResponse.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            var data = TempData["genders"]?.ToString();
            if (data != null)
            {
                var genders = JsonSerializer.Deserialize<List<SelectListItem>>(data);
                appUserRegisterInput.Genders = new SelectList(genders, "Value", "Text");
            }
            return View(appUserRegisterInput);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
