using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.CustomDescriber
{
    public class CustomErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new()
            {
                Code = "PasswordTooShort",
                Description = $"Password must be at least {length} characters long."
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new()
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = "Password must contain at least one alphanumeric (~! etc.) character."
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new()
            {
                Code = "DuplicateUserName",
                Description = $"{userName} is already registered in the system."
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new()
            {
                Code = "DuplicateEmail",
                Description = $"{email} is already registered in the system."
            };
        }
    }
}
