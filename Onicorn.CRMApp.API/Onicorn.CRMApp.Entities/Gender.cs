using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Entities
{
    public class Gender
    {
        [Key]
        public int Id { get; set; }
        public string? Definition { get; set; }
        public List<AppUser>? ProjectUsers { get; set; }
    }
}
