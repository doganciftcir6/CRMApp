using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public int SalesAmount { get; set; }
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
