using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Entities
{
    public class SaleSituation
    {
        public int Id { get; set; }
        public string? Definition { get; set; }
        public List<Sale>? Sales { get; set; }
    }
}
