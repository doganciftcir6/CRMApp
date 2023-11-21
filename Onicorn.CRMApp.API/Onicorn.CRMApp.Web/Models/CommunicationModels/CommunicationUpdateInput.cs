using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Onicorn.CRMApp.Web.Models.CommunicationModels
{
    public class CommunicationUpdateInput
    {
        [Required(ErrorMessage = "The id field is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The communication date field is required.")]
        public DateTime CommunicationDate { get; set; }
        public string? Detail { get; set; }
        [Required(ErrorMessage = "The customer field is required.")]
        public int CustomerId { get; set; }
        public string? Customer { get; set; }
        public SelectList? Customers { get; set; }
        [Required(ErrorMessage = "The communication type field is required.")]
        public int CommunicationTypeId { get; set; }
        public string? CommunicationType { get; set; }
        public SelectList? CommunicationTypes { get; set; }
        public bool Status { get; set; }
    }
}
