using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Dtos.SaleDtos
{
    public class SalesDto
    {
        public int Id { get; set; }
        public decimal SalesAmount { get; set; }
        public DateTime SalesDate { get; set; }
        public bool Status { get; set; }


        public string? Customer { get; set; }
        public string? Project { get; set; }
        public string? SaleSituation { get; set; }
    }
}
