using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Dtos.CommunicationDtos
{
    public class CommunicationCreateDto
    {
        public DateTime CommunicationDate { get; set; }
        public string? Detail { get; set; }
        [JsonIgnore]
        public DateTime InsertTime { get; set; }

        public int CustomerId { get; set; }
        public int CommunicationTypeId { get; set; }
    }
}
