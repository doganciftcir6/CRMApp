using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Onicorn.CRMApp.Web.Models.CommunicationModels
{
    public class CommunicationCreateInput
    {
        [Required(ErrorMessage = "The communication date field is required.")]
        public DateTime CommunicationDate { get; set; }
        [Required(ErrorMessage = "The detail field is required.")]
        public string? Detail { get; set; }
        [Required(ErrorMessage = "The customer field is required.")]
        public int CustomerId { get; set; }
        public SelectList? Customers { get; set; }
        [Required(ErrorMessage = "The communication type field is required.")]
        public int CommunicationTypeId { get; set; }
        public SelectList? CommunicationTypes { get; set; }
    }
}
