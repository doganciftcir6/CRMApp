using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Shared.Utilities.Response;
using Onicorn.CRMApp.Web.Models;
using System.Net;

namespace Onicorn.CRMApp.Web.Controllers
{
    [Authorize]
    public class AppUserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AppUserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ProfileDetails()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync("AppUser/GetProfile");
                if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                    ModelState.AddModelError("", "Server error. Please try again later.");


                CustomResponse<AppUserProfileVM> appUserProfileVM = await response.Content.ReadFromJsonAsync<CustomResponse<AppUserProfileVM>>();
                if (appUserProfileVM.IsSuccessful)
                {
                    return View(appUserProfileVM.Data);
                }
                foreach (var error in appUserProfileVM.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View();
        }

        public async Task<IActionResult> UpdateProfile()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync("AppUser/GetProfile");
                if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                    ModelState.AddModelError("", "Server error. Please try again later.");


                CustomResponse<AppUserUpdateProfileInput> appUserProfileVM = await response.Content.ReadFromJsonAsync<CustomResponse<AppUserUpdateProfileInput>>();
                if (appUserProfileVM.IsSuccessful)
                {
                    return View(appUserProfileVM.Data);
                }
                foreach (var error in appUserProfileVM.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(AppUserUpdateProfileInput appUserUpdateProfileInput)
        {
            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token != null)
                {
                    var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var multipartContent = new MultipartFormDataContent();
                    if (appUserUpdateProfileInput.ImageFile is not null)
                    {
                        using var ms = new MemoryStream();
                        ms.Position = 0;
                        await appUserUpdateProfileInput.ImageFile.CopyToAsync(ms);
                        multipartContent.Add(new ByteArrayContent(ms.ToArray()), "ImageURL", appUserUpdateProfileInput.ImageFile.FileName);
                    }
                    multipartContent.Add(new StringContent(appUserUpdateProfileInput.Firstname), "Firstname");
                    multipartContent.Add(new StringContent(appUserUpdateProfileInput.Lastname), "Lastname");
                    multipartContent.Add(new StringContent(appUserUpdateProfileInput.Email), "Email");
                    multipartContent.Add(new StringContent(appUserUpdateProfileInput.PhoneNumber.ToString()), "PhoneNumber");

                    var response = await _httpClient.PutAsync("AppUser/UpdateProfile", multipartContent);
                    if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                        ModelState.AddModelError("", "Server error. Please try again later.");

                    CustomResponse<NoContent> updateProfileResponse = await response.Content.ReadFromJsonAsync<CustomResponse<NoContent>>();
                    if (updateProfileResponse.IsSuccessful)
                    {
                        return RedirectToAction(nameof(ProfileDetails));
                    }
                    foreach (var error in updateProfileResponse.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(appUserUpdateProfileInput);
        }

        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(AppUserChangePasswordInput appUserChangePasswordInput)
        {
            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token != null)
                {
                    var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var response = await _httpClient.PutAsJsonAsync("AppUser/ChangePassword", appUserChangePasswordInput);
                    if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                        ModelState.AddModelError("", "Server error. Please try again later.");

                    CustomResponse<NoContent> changePasswordResponse = await response.Content.ReadFromJsonAsync<CustomResponse<NoContent>>();
                    if (changePasswordResponse.IsSuccessful)
                    {
                        TempData["success"] = "Password changed successfully";
                        return View();
                    }
                    foreach (var error in changePasswordResponse.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(appUserChangePasswordInput);
        }
    }
}
