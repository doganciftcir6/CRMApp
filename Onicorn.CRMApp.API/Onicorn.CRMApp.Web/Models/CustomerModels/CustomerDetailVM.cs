namespace Onicorn.CRMApp.Web.Models.CustomerModels
{
    public class CustomerDetailVM
    {
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Status { get; set; }
    }
}
