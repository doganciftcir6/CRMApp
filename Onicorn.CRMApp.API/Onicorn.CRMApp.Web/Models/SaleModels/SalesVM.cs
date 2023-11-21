namespace Onicorn.CRMApp.Web.Models.SaleModels
{
    public class SalesVM
    {
        public int Id { get; set; }
        public decimal SalesAmount { get; set; }
        public int KDV { get; set; }
        public decimal TotalSalesAmount { get; set; }
        public DateTime SalesDate { get; set; }
        public bool Status { get; set; }


        public string? Customer { get; set; }
        public string? Project { get; set; }
        public string? SaleSituation { get; set; }
    }
}
