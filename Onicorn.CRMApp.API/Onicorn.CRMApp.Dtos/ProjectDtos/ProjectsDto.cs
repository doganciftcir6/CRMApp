using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Dtos.ProjectDtos
{
    public class ProjectsDto
    {
        public int Id { get; set; }
        public string? ProjectName { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? ImageURL { get; set; }
        public bool Status { get; set; }
    }
}
