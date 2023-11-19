using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Onicorn.CRMApp.Shared.Utilities.Response;
using Onicorn.CRMApp.Web.Models;
using System.Net;
using System.Text;
using System.Text.Json;

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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> InsertSale()
        {
            SaleCreateInput saleCreateInput = new SaleCreateInput();
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var customerResponse = await _httpClient.GetAsync("Customer/GetCustomers");
                var projectResponse = await _httpClient.GetAsync("Project/GetProjects");
                var saleSituationResponse = await _httpClient.GetAsync("SaleSituation/GetSaleSituations");
                if (projectResponse.IsSuccessStatusCode && customerResponse.IsSuccessStatusCode && saleSituationResponse.IsSuccessStatusCode)
                {
                    CustomResponse<IEnumerable<ProjectsVM>> projects = await projectResponse.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<ProjectsVM>>>();
                    CustomResponse<IEnumerable<CustomersVM>> customers = await customerResponse.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<CustomersVM>>>();
                    CustomResponse<IEnumerable<SaleSituationVM>> saleSituations = await saleSituationResponse.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<SaleSituationVM>>>();

                    saleCreateInput.Customers = new SelectList(customers.Data, "Id", "CompanyName");
                    saleCreateInput.Projects = new SelectList(projects.Data, "Id", "ProjectName");
                    saleCreateInput.SaleSituations = new SelectList(saleSituations.Data, "Id", "Definition");
                }
            }
            return View(saleCreateInput);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> InsertSale(SaleCreateInput saleCreateInput)
        {
            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token != null)
                {
                    var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var response = await _httpClient.PostAsJsonAsync("Sale/InsertSale", saleCreateInput);
                    if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError("", "Server error. Please try again later.");
                        return View(saleCreateInput);
                    }

                    CustomResponse<NoContent> saleCreateResponse = await response.Content.ReadFromJsonAsync<CustomResponse<NoContent>>();
                    if (saleCreateResponse.IsSuccessful)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var error in saleCreateResponse.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            var customersData = TempData["customers"]?.ToString();
            var projectsData = TempData["projects"]?.ToString();
            var saleSituationsData = TempData["saleSituations"]?.ToString();
            if (customersData != null || projectsData != null || saleSituationsData != null)
            {
                var customers = JsonSerializer.Deserialize<List<SelectListItem>>(customersData);
                var projects = JsonSerializer.Deserialize<List<SelectListItem>>(projectsData);
                var saleSituations = JsonSerializer.Deserialize<List<SelectListItem>>(saleSituationsData);
                saleCreateInput.Customers = new SelectList(customers, "Value", "Text");
                saleCreateInput.Projects = new SelectList(projects, "Value", "Text");
                saleCreateInput.SaleSituations = new SelectList(saleSituations, "Value", "Text");
            }
            return View(saleCreateInput);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSale(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"Sale/GetSaleById/{id}");
                var customerResponse = await _httpClient.GetAsync("Customer/GetCustomers");
                var projectResponse = await _httpClient.GetAsync("Project/GetProjects");
                var saleSituationResponse = await _httpClient.GetAsync("SaleSituation/GetSaleSituations");
                if (response.IsSuccessStatusCode && customerResponse.IsSuccessStatusCode && customerResponse.IsSuccessStatusCode && saleSituationResponse.IsSuccessStatusCode)
                {
                    CustomResponse<SaleUpdateInput> sale = await response.Content.ReadFromJsonAsync<CustomResponse<SaleUpdateInput>>();
                    CustomResponse<IEnumerable<ProjectsVM>> projects = await projectResponse.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<ProjectsVM>>>();
                    CustomResponse<IEnumerable<CustomersVM>> customers = await customerResponse.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<CustomersVM>>>();
                    CustomResponse<IEnumerable<SaleSituationVM>> saleSituations = await saleSituationResponse.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<SaleSituationVM>>>();

                    sale.Data.Customers = new SelectList(customers.Data, "Id", "CompanyName");
                    sale.Data.Projects = new SelectList(projects.Data, "Id", "ProjectName");
                    sale.Data.SaleSituations = new SelectList(saleSituations.Data, "Id", "Definition");

                    return View(sale.Data);
                }
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateSale(SaleUpdateInput saleUpdateInput)
        {
            if (ModelState.IsValid)
            {

                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token != null)
                {
                    var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var response = await _httpClient.PutAsJsonAsync("Sale/UpdateSale", saleUpdateInput);
                    if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError("", "Server error. Please try again later.");
                        return View(saleUpdateInput);
                    }

                    CustomResponse<NoContent> saleUpdateResponse = await response.Content.ReadFromJsonAsync<CustomResponse<NoContent>>();
                    if (saleUpdateResponse.IsSuccessful)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var error in saleUpdateResponse.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            var customersData = TempData["customers"]?.ToString();
            var projectsData = TempData["projects"]?.ToString();
            var saleSituationsData = TempData["saleSituations"]?.ToString();
            if (customersData != null || projectsData != null || saleSituationsData != null)
            {
                var customers = JsonSerializer.Deserialize<List<SelectListItem>>(customersData);
                var projects = JsonSerializer.Deserialize<List<SelectListItem>>(projectsData);
                var saleSituations = JsonSerializer.Deserialize<List<SelectListItem>>(saleSituationsData);
                saleUpdateInput.Customers = new SelectList(customers, "Value", "Text");
                saleUpdateInput.Projects = new SelectList(projects, "Value", "Text");
                saleUpdateInput.SaleSituations = new SelectList(saleSituations, "Value", "Text");
            }
            return View(saleUpdateInput);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveSale(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var jsonContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsJsonAsync($"Sale/RemoveSale/{id}", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return NotFound();
        }
    }
}
