using Microsoft.AspNetCore.Mvc;

namespace Onicorn.CRMApp.Web.ViewComponents
{
    public class LogoutModalComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
