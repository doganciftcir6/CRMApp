using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Dtos.TaskDtos
{
    public class TasksDto
    {
        public int Id { get; set; }
        public string? Taskname { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool Status { get; set; }


        public string? TaskSituation { get; set; }
        public string? AppUser { get; set; }
    }
}
