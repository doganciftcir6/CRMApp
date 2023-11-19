using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Shared.Utilities.Response;
using Onicorn.CRMApp.Web.Models;

namespace Onicorn.CRMApp.Web.Controllers
{
    public class SaleController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SaleController(IHttpClientFactory httpClientFactory)
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
                var response = await _httpClient.GetAsync("Sale/GetSales");
                if (response.IsSuccessStatusCode)
                {
                    CustomResponse<IEnumerable<SalesVM>> salesResponse = await response.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<SalesVM>>>();
                    return View(salesResponse.Data);
                }
            }
            return View();
        }

    }
}
