using System.ComponentModel.DataAnnotations;

namespace Onicorn.CRMApp.Web.Models.AppUserModels
{
    public class AppUserUpdateProfileInput
    {
        [Required(ErrorMessage = "The firstname field is required.")]
        public string? Firstname { get; set; }
        [Required(ErrorMessage = "The lastname field is required.")]
        public string? Lastname { get; set; }
        [Required(ErrorMessage = "The phone number field is required.")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "The email field is required.")]
        public string? Email { get; set; }
        public string? ImageURL { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
