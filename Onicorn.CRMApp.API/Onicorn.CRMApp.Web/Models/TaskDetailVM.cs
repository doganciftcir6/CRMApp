namespace Onicorn.CRMApp.Web.Models
{
    public class TaskDetailVM
    {
        public int Id { get; set; }
        public string? Taskname { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool Status { get; set; }


        public string? TaskSituation { get; set; }
        public string? AppUser { get; set; }
    }
}
