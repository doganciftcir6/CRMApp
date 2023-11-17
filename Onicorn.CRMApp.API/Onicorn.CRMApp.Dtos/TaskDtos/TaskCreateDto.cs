using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Dtos.TaskDtos
{
    public class TaskCreateDto
    {
        public string? Taskname { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }

        public int TaskSituationId { get; set; }
        public int AppUserId { get; set; }
    }
}
