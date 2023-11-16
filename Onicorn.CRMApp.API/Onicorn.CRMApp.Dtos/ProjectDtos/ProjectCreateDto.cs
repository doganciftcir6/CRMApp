using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Onicorn.CRMApp.Dtos.ProjectDtos
{
    public class ProjectCreateDto
    {
        public string? ProjectName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile? ImageURL { get; set; }
        [JsonIgnore]
        public DateTime InsertTime { get; set; }
    }
}
