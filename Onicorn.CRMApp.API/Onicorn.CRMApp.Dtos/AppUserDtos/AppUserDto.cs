using Onicorn.CRMApp.Dtos.GenderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Dtos.AppUserDtos
{
    public class AppUserDto
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? ImageURL { get; set; }

        public int GenderId { get; set; }
        public GenderDto? Gender { get; set; }
    }
}
