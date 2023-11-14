using Microsoft.AspNetCore.Mvc;
using Onicorn.CRMApp.Shared.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Shared.ControllerBases
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(CustomResponse<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
        }
    }
}
