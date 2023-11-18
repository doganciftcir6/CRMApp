using System.ComponentModel.DataAnnotations;

namespace Onicorn.CRMApp.Web.Models
{
    public class AppUserLoginInput
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "The user name field is required.")]
        public string Username { get; set; } = null!;
        [Display(Name = "Password")]
        [Required(ErrorMessage = "The password field is required.")]
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }
    }
}
