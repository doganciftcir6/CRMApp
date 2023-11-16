using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Dtos.CommunicationDtos
{
    public class CommunicationUpdateDto
    {
        public int Id { get; set; }
        public DateTime CommunicationDate { get; set; }
        public string? Detail { get; set; }
        [JsonIgnore]
        public DateTime UpdateTime { get; set; }
        public bool Status { get; set; }

        public int CustomerId { get; set; }
        public int CommunicationTypeId { get; set; }
    }
}
