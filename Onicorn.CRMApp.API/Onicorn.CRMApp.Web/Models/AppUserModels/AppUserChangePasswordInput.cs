using System.ComponentModel.DataAnnotations;

namespace Onicorn.CRMApp.Web.Models.AppUserModels
{
    public class AppUserChangePasswordInput
    {
        [Required(ErrorMessage = "The current password field is required.")]
        public string? CurrentPassword { get; set; }
        [Required(ErrorMessage = "The new password field is required.")]
        public string? NewPassword { get; set; }
        [Required(ErrorMessage = "The confirm password field is required.")]
        [Compare(nameof(NewPassword), ErrorMessage = "The new password and confirm password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
