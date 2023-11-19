using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Onicorn.CRMApp.Web.Models
{
    public class SaleCreateInput
    {
        [Required(ErrorMessage = "The sales amount field is required.")]
        public decimal SalesAmount { get; set; }
        [Required(ErrorMessage = "The sales date field is required.")]
        public DateTime SalesDate { get; set; }

        [Required(ErrorMessage = "The customer field is required.")]
        public int CustomerId { get; set; }
        public SelectList? Customers { get; set; }
        [Required(ErrorMessage = "The project field is required.")]
        public int ProjectId { get; set; }
        public SelectList? Projects { get; set; }
        [Required(ErrorMessage = "The sale situation field is required.")]
        public int SaleSituationId { get; set; }
        public SelectList? SaleSituations { get; set; }
    }
}
