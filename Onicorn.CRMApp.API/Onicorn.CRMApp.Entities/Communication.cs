using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Entities
{
    public class Communication
    {
        [Key]
        public int Id { get; set; }
        public DateTime CommunicationDate { get; set; }
        public string? Detail { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Status { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int CommunicationTypeId { get; set; }
        public CommunicationType? CommunicationType { get; set; }
    }
}
