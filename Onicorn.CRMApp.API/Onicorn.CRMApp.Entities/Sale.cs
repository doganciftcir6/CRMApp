using System.ComponentModel.DataAnnotations;

namespace Onicorn.CRMApp.Entities
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public decimal SalesAmount { get; set; }
        public DateTime SalesDate { get; set; }
        public bool Status { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public int SaleSituationId { get; set; }
        public SaleSituation? SaleSituation { get; set; }
    }
}
