using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Web.Models;
using System.Diagnostics;

namespace Onicorn.CRMApp.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}