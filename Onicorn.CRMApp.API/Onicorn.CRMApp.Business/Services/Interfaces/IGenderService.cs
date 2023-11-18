using Onicorn.CRMApp.Dtos.GenderDtos;
using Onicorn.CRMApp.Shared.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Services.Interfaces
{
    public interface IGenderService
    {
        Task<CustomResponse<IEnumerable<GenderDto>>> GetGendersAsync();
    }
}
