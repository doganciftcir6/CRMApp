using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Onicorn.CRMApp.Web.Models
{
    public class AppUserRegisterInput
    {
        [Required(ErrorMessage = "The firstname field is required.")]
        public string? Firstname { get; set; }
        [Required(ErrorMessage = "The lastname field is required.")]
        public string? Lastname { get; set; }
        [Required(ErrorMessage = "The phone number field is required.")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "The email field is required.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "The user name field is required.")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "The password field is required.")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "The confirm password field is required.")]
        [Compare(nameof(Password), ErrorMessage = "The password and confirm password do not match.")]
        public string? ConfirmPassword { get; set; }
        public IFormFile? ImageURL { get; set; }
        [Required(ErrorMessage = "The gender field is required.")]
        public int GenderId { get; set; }
        public SelectList? Genders { get; set; }
    }
}
