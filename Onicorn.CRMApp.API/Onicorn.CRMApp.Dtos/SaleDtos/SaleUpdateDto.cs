namespace Onicorn.CRMApp.Dtos.SaleDtos
{
    public class SaleUpdateDto 
    {
        public int Id { get; set; }
        public decimal SalesAmount { get; set; }
        public DateTime? SalesDate { get; set; }
        public bool Status { get; set; }

        public int CustomerId { get; set; }
        public int ProjectId { get; set; }
        public int SaleSituationId { get; set; }
    }
}
