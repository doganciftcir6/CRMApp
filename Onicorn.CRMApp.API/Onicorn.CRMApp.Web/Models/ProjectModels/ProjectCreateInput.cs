using System.ComponentModel.DataAnnotations;

namespace Onicorn.CRMApp.Web.Models.ProjectModels
{
    public class ProjectCreateInput
    {
        [Required(ErrorMessage = "The project name required.")]
        public string? ProjectName { get; set; }
        [Required(ErrorMessage = "The description required.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "The price required.")]
        public decimal Price { get; set; }
        public IFormFile? ImageURL { get; set; }
    }
}
