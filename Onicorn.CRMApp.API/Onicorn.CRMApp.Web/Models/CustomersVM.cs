namespace Onicorn.CRMApp.Web.Models
{
    public class CustomersVM
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Status { get; set; }
    }
}
