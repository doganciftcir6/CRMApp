using AutoMapper;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.Repositories.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.CommunicationDtos;
using Onicorn.CRMApp.Entities;
using Onicorn.CRMApp.Shared.Utilities.Response;
using Onicorn.CRMApp.Shared.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Services.Concrete
{
    public class CommunicationService : ICommunicationService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly ICommunicationRepository _communicationRepository;
        public CommunicationService(IUow uow, IMapper mapper, ICommunicationRepository communicationRepository)
        {
            _uow = uow;
            _mapper = mapper;
            _communicationRepository = communicationRepository;
        }

        public async Task<CustomResponse<IEnumerable<CommunicationDto>>> GetCommunicatios()
        {
            var communicationDto = _mapper.Map<IEnumerable<CommunicationDto>>(await _communicationRepository.GetAllAsync());
            return CustomResponse<IEnumerable<CommunicationDto>>.Success(communicationDto, ResponseStatusCode.OK);
        }
    }
}
