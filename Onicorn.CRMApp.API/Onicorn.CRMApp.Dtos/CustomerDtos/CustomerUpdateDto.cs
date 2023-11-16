using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Dtos.CustomerDtos
{
    public class CustomerUpdateDto
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public bool Status { get; set; }
        [JsonIgnore]
        public DateTime UpdateTime { get; set; }
    }
}
