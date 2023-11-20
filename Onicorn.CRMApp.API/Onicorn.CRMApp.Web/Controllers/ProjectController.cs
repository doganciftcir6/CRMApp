using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Shared.Utilities.Response;
using Onicorn.CRMApp.Web.Models;
using System.Net;

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

        public async Task<IActionResult> ProjectDetails(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"Project/GetProject/{id}");
                if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    ModelState.AddModelError("", "Server error. Please try again later.");
                    return View();
                }

                CustomResponse<ProjectDetailVM> projectDetailResponse = await response.Content.ReadFromJsonAsync<CustomResponse<ProjectDetailVM>>();
                if (projectDetailResponse.IsSuccessful)
                {
                    return View(projectDetailResponse.Data);
                }
                foreach (var error in projectDetailResponse.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> InsertProject()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> InsertProject(ProjectCreateInput projectCreateInput)
        {
            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token != null)
                {
                    var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var multipartContent = new MultipartFormDataContent();
                    if (projectCreateInput.ImageURL is not null)
                    {
                        using var ms = new MemoryStream();
                        ms.Position = 0;
                        await projectCreateInput.ImageURL.CopyToAsync(ms);
                        multipartContent.Add(new ByteArrayContent(ms.ToArray()), "ImageURL", projectCreateInput.ImageURL.FileName);
                    }
                    multipartContent.Add(new StringContent(projectCreateInput.ProjectName), "ProjectName");
                    multipartContent.Add(new StringContent(projectCreateInput.Description), "Description");
                    multipartContent.Add(new StringContent(projectCreateInput.Price.ToString()), "Price");

                    var response = await _httpClient.PostAsync("Project/InsertProject", multipartContent);
                    if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError("", "Server error. Please try again later.");
                        return View(projectCreateInput);
                    }

                    CustomResponse<NoContent> insertProjectResponse = await response.Content.ReadFromJsonAsync<CustomResponse<NoContent>>();
                    if (insertProjectResponse.IsSuccessful)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var error in insertProjectResponse.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(projectCreateInput);
        }
    }
}
