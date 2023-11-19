using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Onicorn.CRMApp.Entities;
using Onicorn.CRMApp.Shared.Utilities.Response;
using Onicorn.CRMApp.Web.Models;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> InsertCommunication()
        {
            CommunicationCreateInput communicationCreateInput = new CommunicationCreateInput();
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var customerResponse = await _httpClient.GetAsync("Customer/GetCustomers");
                var communicationTypeResponse = await _httpClient.GetAsync("CommunicationType/GetCommunicationTypes");
                if (communicationTypeResponse.IsSuccessStatusCode && customerResponse.IsSuccessStatusCode)
                {
                    CustomResponse<IEnumerable<CommunicationTypeVM>> communicationTypes = await communicationTypeResponse.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<CommunicationTypeVM>>>();
                    CustomResponse<IEnumerable<CustomersVM>> customers = await customerResponse.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<CustomersVM>>>();

                    communicationCreateInput.Customers = new SelectList(customers.Data, "Id", "CompanyName");
                    communicationCreateInput.CommunicationTypes = new SelectList(communicationTypes.Data, "Id", "Definition");
                }
            }
            return View(communicationCreateInput);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> InsertCommunication(CommunicationCreateInput communicationCreateInput)
        {
            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token != null)
                {
                    var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var response = await _httpClient.PostAsJsonAsync("Communication/InsertCommunication", communicationCreateInput);
                    if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError("", "Server error. Please try again later.");
                        return View(communicationCreateInput);
                    }

                    CustomResponse<NoContent> communicationCreateResponse = await response.Content.ReadFromJsonAsync<CustomResponse<NoContent>>();
                    if (communicationCreateResponse.IsSuccessful)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var error in communicationCreateResponse.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            var customersData = TempData["customers"]?.ToString();
            var communicationTypesData = TempData["communicationTypes"]?.ToString();
            if (customersData != null || communicationTypesData != null)
            {
                var customers = JsonSerializer.Deserialize<List<SelectListItem>>(customersData);
                var communicationTypes = JsonSerializer.Deserialize<List<SelectListItem>>(communicationTypesData);
                communicationCreateInput.Customers = new SelectList(customers, "Value", "Text");
                communicationCreateInput.CommunicationTypes = new SelectList(communicationTypes, "Value", "Text");
            }
            return View(communicationCreateInput);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCommunication(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"Communication/GetCommunicationById/{id}");
                var customerResponse = await _httpClient.GetAsync("Customer/GetCustomers");
                var communicationTypeResponse = await _httpClient.GetAsync("CommunicationType/GetCommunicationTypes");
                if (response.IsSuccessStatusCode && communicationTypeResponse.IsSuccessStatusCode && customerResponse.IsSuccessStatusCode)
                {
                    CustomResponse<CommunicationUpdateInput> communication = await response.Content.ReadFromJsonAsync<CustomResponse<CommunicationUpdateInput>>();
                    CustomResponse<IEnumerable<CommunicationTypeVM>> communicationTypes = await communicationTypeResponse.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<CommunicationTypeVM>>>();
                    CustomResponse<IEnumerable<CustomersVM>> customers = await customerResponse.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<CustomersVM>>>();

                    communication.Data.Customers = new SelectList(customers.Data, "Id", "CompanyName");
                    communication.Data.CommunicationTypes = new SelectList(communicationTypes.Data, "Id", "Definition");

                    return View(communication.Data);
                }
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCommunication(CommunicationUpdateInput communicationUpdateInput)
        {
            if (ModelState.IsValid)
            {

                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token != null)
                {
                    var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var response = await _httpClient.PutAsJsonAsync("Communication/UpdateCommunication", communicationUpdateInput);
                    if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError("", "Server error. Please try again later.");
                        return View(communicationUpdateInput);
                    }

                    CustomResponse<NoContent> communicationUpdateResponse = await response.Content.ReadFromJsonAsync<CustomResponse<NoContent>>();
                    if (communicationUpdateResponse.IsSuccessful)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var error in communicationUpdateResponse.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            var customersData = TempData["customers"]?.ToString();
            var communicationTypesData = TempData["communicationTypes"]?.ToString();
            if (customersData != null || communicationTypesData != null)
            {
                var customers = JsonSerializer.Deserialize<List<SelectListItem>>(customersData);
                var communicationTypes = JsonSerializer.Deserialize<List<SelectListItem>>(communicationTypesData);
                communicationUpdateInput.Customers = new SelectList(customers, "Value", "Text");
                communicationUpdateInput.CommunicationTypes = new SelectList(communicationTypes, "Value", "Text");
            }
            return View(communicationUpdateInput);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveCommunication(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var jsonContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsJsonAsync($"Communication/DeleteCommunication/{id}", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return NotFound();
        }
    }
}
