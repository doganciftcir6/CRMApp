using Microsoft.AspNetCore.Mvc;

namespace Onicorn.CRMApp.Web.ViewComponents
{
    public class FooterComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
