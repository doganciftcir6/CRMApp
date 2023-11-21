namespace Onicorn.CRMApp.Web.Models.AppUserModels
{
    public class AppUserLoginVM
    {
        public string? Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
