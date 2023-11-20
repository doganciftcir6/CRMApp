namespace Onicorn.CRMApp.Web.Models
{
    public class ProjectDetailVM
    {
        public string? ProjectName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageURL { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool Status { get; set; }
    }
}
