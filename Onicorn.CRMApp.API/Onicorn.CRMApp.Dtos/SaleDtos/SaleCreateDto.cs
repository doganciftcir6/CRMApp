using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Dtos.SaleDtos
{
    public class SaleCreateDto
    {
        public decimal SalesAmount { get; set; }
        public DateTime SalesDate { get; set; }

        public int CustomerId { get; set; }
        public int ProjectId { get; set; }
        public int SaleSituationId { get; set; }
    }
}
