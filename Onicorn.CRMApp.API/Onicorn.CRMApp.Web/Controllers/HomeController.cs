using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Shared.Utilities.Response;
using Onicorn.CRMApp.Web.Models;
using Onicorn.CRMApp.Web.Models.StatisticModels;
using System.Diagnostics;
using System.Net.Http;

namespace Onicorn.CRMApp.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(IHttpClientFactory httpClientFactory)
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
                var response = await _httpClient.GetAsync("Statistic/GetStatistics");
                if (response.IsSuccessStatusCode)
                {
                    CustomResponse<StatisticVM> statisticResponse = await response.Content.ReadFromJsonAsync<CustomResponse<StatisticVM>>();
                    return View(statisticResponse.Data);
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}