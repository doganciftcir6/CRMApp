using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Dtos.ProjectDtos
{
    public class ProjectUpdateDto
    {
        public int Id { get; set; }
        public string? ProjectName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public IFormFile? ImageURL { get; set; }
        [JsonIgnore]
        public DateTime UpdateTime { get; set; }
    }
}
