using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Shared.Utilities.Response;
using Onicorn.CRMApp.Web.Models;
using System.Net.Http;

namespace Onicorn.CRMApp.Web.Controllers
{
    [Authorize]
    public class CommunicationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CommunicationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync("Communication/GetCommunications");
                if (response.IsSuccessStatusCode)
                {
                    CustomResponse<IEnumerable<CommunicationsVM>> communicationsResponse = await response.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<CommunicationsVM>>>();
                    return View(communicationsResponse.Data);
                }
            }
            return View();
        }
    }
}
