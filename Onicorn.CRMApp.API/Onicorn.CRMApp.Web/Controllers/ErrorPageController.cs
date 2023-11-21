using Microsoft.AspNetCore.Mvc;

namespace Onicorn.CRMApp.Web.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Error404()
        {
            return View();
        }
    }
}
