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
                    ModelState.AddModelError("", "Authentication server error. Please try again later.");


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
    }
}
