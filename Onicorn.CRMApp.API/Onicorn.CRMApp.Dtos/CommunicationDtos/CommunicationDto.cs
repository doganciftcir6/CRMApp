using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Dtos.CommunicationDtos
{
    public class CommunicationDto
    {
        public DateTime CommunicationDate { get; set; }
        public string? Detail { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Status { get; set; }

        public string? Customer { get; set; }
        public string? CommunicationType { get; set; }
    }
}
