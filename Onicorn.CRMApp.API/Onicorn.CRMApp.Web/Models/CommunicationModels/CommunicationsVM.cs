namespace Onicorn.CRMApp.Web.Models.CommunicationModels
{
    public class CommunicationsVM
    {
        public int Id { get; set; }
        public DateTime CommunicationDate { get; set; }
        public string? Detail { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Status { get; set; }

        public string? Customer { get; set; }
        public string? CommunicationType { get; set; }
    }
}
