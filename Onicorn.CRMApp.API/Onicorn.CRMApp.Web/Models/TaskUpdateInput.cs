using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Onicorn.CRMApp.Web.Models
{
    public class TaskUpdateInput
    {
        [Required(ErrorMessage = "The id field is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The task name field is required.")]
        public string? Taskname { get; set; }
        [Required(ErrorMessage = "The description field is required.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "The start date field is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The task situation field is required.")]
        public int TaskSituationId { get; set; }
        public SelectList? TaskSituations { get; set; }
        [Required(ErrorMessage = "The app user field is required.")]
        public int AppUserId { get; set; }
        public SelectList? AppUsers { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool Status { get; set; }
    }
}
