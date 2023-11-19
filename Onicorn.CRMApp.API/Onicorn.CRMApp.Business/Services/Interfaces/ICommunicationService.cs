using Onicorn.CRMApp.Dtos.CommunicationDtos;
using Onicorn.CRMApp.Shared.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Services.Interfaces
{
    public interface ICommunicationService
    {
        Task<CustomResponse<IEnumerable<CommunicationDto>>> GetCommunicatiosAsync();
        Task<CustomResponse<CommunicationDto>> GetCommunicationByIdAsync(int id);
        Task<CustomResponse<NoContent>> InsertCommunicationAsync(CommunicationCreateDto communicationCreateDto);
        Task<CustomResponse<NoContent>> UpdateCommunicationAsync(CommunicationUpdateDto communicationUpdateDto);
        Task<CustomResponse<NoContent>> RemoveCommunicationAsync(int communicationId);
    }
}
