namespace Onicorn.CRMApp.Web.Models.ProjectModels
{
    public class ProjectsVM
    {
        public int Id { get; set; }
        public string? ProjectName { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? ImageURL { get; set; }
        public bool Status { get; set; }
    }
}
