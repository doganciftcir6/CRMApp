using System.ComponentModel.DataAnnotations;

namespace Onicorn.CRMApp.Web.Models.CustomerModels
{
    public class CustomerUpdateInput
    {
        [Required(ErrorMessage = "The id field is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The company name field is required.")]
        public string? CompanyName { get; set; }
        [Required(ErrorMessage = "The email required.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "The phone is required.")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "The address is required.")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "The province is required.")]
        public string? Province { get; set; }
        [Required(ErrorMessage = "The district field is required.")]
        public string? District { get; set; }
        public bool Status { get; set; }
    }
}
