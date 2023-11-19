using AutoMapper;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.CommunicationTypeDtos;
using Onicorn.CRMApp.Entities;
using Onicorn.CRMApp.Shared.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Services.Concrete
{
    public class CommunicationTypeService : ICommunicationTypeService
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        public CommunicationTypeService(IMapper mapper, IUow uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<CustomResponse<IEnumerable<CommunicationTypesDto>>> GetCommunicationTypesAsync()
        {
            IEnumerable<CommunicationTypesDto> communicationTypesDto = _mapper.Map<IEnumerable<CommunicationTypesDto>>(await _uow.GetRepository<CommunicationType>().GetAllAsync());
            return CustomResponse<IEnumerable<CommunicationTypesDto>>.Success(communicationTypesDto, ResponseStatusCode.OK);
        }
    }
}
