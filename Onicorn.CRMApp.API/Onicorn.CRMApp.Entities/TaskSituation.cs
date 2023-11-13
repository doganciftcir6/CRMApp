using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Entities
{
    public class TaskSituation
    {
        [Key]
        public int Id { get; set; }
        public string? Definition { get; set; }
        public List<Task>? Tasks { get; set; }
    }
}
