using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? ImageURL { get; set; }

        public int GenderId { get; set; }
        public Gender? Gender { get; set; }

        public List<Task>? Tasks { get; set; }
    }
}
