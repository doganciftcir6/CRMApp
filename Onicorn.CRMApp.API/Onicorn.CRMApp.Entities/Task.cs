using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string? Taskname { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Status { get; set; }

        public int TaskSituationId { get; set; }
        public TaskSituation? TaskSituation { get; set; }

        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
