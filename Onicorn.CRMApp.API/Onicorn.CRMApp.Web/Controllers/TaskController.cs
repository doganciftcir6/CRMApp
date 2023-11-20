using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Shared.Utilities.Response;
using Onicorn.CRMApp.Web.Models;

namespace Onicorn.CRMApp.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TaskController(IHttpClientFactory httpClientFactory)
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
                var response = await _httpClient.GetAsync("Task/GetTasks");
                if (response.IsSuccessStatusCode)
                {
                    CustomResponse<IEnumerable<TasksVM>> tasksResponse = await response.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<TasksVM>>>();
                    return View(tasksResponse.Data);
                }
            }
            return View();
        }

        public async Task<IActionResult> MyTasks()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync("Task/GetTasksByUser");
                if (response.IsSuccessStatusCode)
                {
                    CustomResponse<IEnumerable<TasksVM>> tasksResponse = await response.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<TasksVM>>>();
                    return View(tasksResponse.Data);
                }
            }
            return View();
        }
    }
}
