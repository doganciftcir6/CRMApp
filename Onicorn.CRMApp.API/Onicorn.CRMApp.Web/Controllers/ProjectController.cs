using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Shared.Utilities.Response;
using Onicorn.CRMApp.Web.Models;

namespace Onicorn.CRMApp.Web.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProjectController(IHttpClientFactory httpClientFactory)
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
                var response = await _httpClient.GetAsync("Project/GetProjects");
                if (response.IsSuccessStatusCode)
                {
                    CustomResponse<IEnumerable<ProjectsVM>> projectsResponse = await response.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<ProjectsVM>>>();
                    return View(projectsResponse.Data);
                }
            }
            return View();
        }
    }
}
