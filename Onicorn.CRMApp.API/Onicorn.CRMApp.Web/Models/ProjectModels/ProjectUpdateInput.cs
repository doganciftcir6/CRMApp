using System.ComponentModel.DataAnnotations;

namespace Onicorn.CRMApp.Web.Models.ProjectModels
{
    public class ProjectUpdateInput
    {
        [Required(ErrorMessage = "The id required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The project name required.")]
        public string? ProjectName { get; set; }
        [Required(ErrorMessage = "The description required.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "The price required.")]
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public string? ImageURL { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
