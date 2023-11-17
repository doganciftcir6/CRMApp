using Microsoft.AspNetCore.Mvc;

namespace Onicorn.CRMApp.Web.ViewComponents
{
    public class TopbarComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
