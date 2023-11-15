using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Shared.Utilities.Services
{
    public interface ISharedIdentityService
    {
        int? GetUserId { get; }
    }
}
