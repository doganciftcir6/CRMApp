using Microsoft.AspNetCore.Mvc;

namespace Onicorn.CRMApp.Web.ViewComponents
{
    public class SidebarComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
