using Onicorn.CRMApp.Dtos.CommunicationTypeDtos;
using Onicorn.CRMApp.Shared.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Services.Interfaces
{
    public interface ICommunicationTypeService
    {
        Task<CustomResponse<IEnumerable<CommunicationTypesDto>>> GetCommunicationTypesAsync();
    }
}
