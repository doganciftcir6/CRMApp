using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Shared.Utilities.Response;
using System.Net.Http;
using System.Net;
using NuGet.Common;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Onicorn.CRMApp.Web.Models.CustomerModels;

namespace Onicorn.CRMApp.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CustomerController(IHttpClientFactory httpClientFactory)
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
                var response = await _httpClient.GetAsync("Customer/GetCustomers");
                if (response.IsSuccessStatusCode)
                {
                    CustomResponse<IEnumerable<CustomersVM>> customersResponse = await response.Content.ReadFromJsonAsync<CustomResponse<IEnumerable<CustomersVM>>>();
                    return View(customersResponse.Data);
                }
            }
            return View();
        }
        public async Task<IActionResult> CustomerDetails(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"Customer/GetCustomer/{id}");
                if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    ModelState.AddModelError("", "Server error. Please try again later.");
                    return View();
                }

                CustomResponse<CustomerDetailVM> customerDetailResponse = await response.Content.ReadFromJsonAsync<CustomResponse<CustomerDetailVM>>();
                if (customerDetailResponse.IsSuccessful)
                {
                    return View(customerDetailResponse.Data);
                }
                foreach (var error in customerDetailResponse.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> InsertCustomer()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> InsertCustomer(CustomerCreateInput customerCreateInput)
        {
            if (ModelState.IsValid)
            {
                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token != null)
                {
                    var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var response = await _httpClient.PostAsJsonAsync("Customer/InsertCustomer", customerCreateInput);
                    if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError("", "Server error. Please try again later.");
                        return View(customerCreateInput);
                    }

                    CustomResponse<NoContent> customerCreateResponse = await response.Content.ReadFromJsonAsync<CustomResponse<NoContent>>();
                    if (customerCreateResponse.IsSuccessful)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var error in customerCreateResponse.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(customerCreateInput);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"Customer/GetCustomer/{id}");
                if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    ModelState.AddModelError("", "Server error. Please try again later.");
                    return View();
                }

                CustomResponse<CustomerUpdateInput> customerDetailResponse = await response.Content.ReadFromJsonAsync<CustomResponse<CustomerUpdateInput>>();
                if (customerDetailResponse.IsSuccessful)
                {
                    return View(customerDetailResponse.Data);
                }
                foreach (var error in customerDetailResponse.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(CustomerUpdateInput customerUpdateInput)
        {
            if (ModelState.IsValid)
            {

                var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
                if (token != null)
                {
                    var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var response = await _httpClient.PutAsJsonAsync("Customer/UpdateCustomer", customerUpdateInput);
                    if (response is null || response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        ModelState.AddModelError("", "Server error. Please try again later.");
                        return View(customerUpdateInput);
                    }

                    CustomResponse<NoContent> customerUpdateResponse = await response.Content.ReadFromJsonAsync<CustomResponse<NoContent>>();
                    if (customerUpdateResponse.IsSuccessful)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var error in customerUpdateResponse.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(customerUpdateInput);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveCustomer(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var _httpClient = _httpClientFactory.CreateClient("MyApiClient");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var jsonContent = new StringContent("{}", Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsJsonAsync($"Customer/RemoveCustomer/{id}", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return NotFound();
        }
    }
}
