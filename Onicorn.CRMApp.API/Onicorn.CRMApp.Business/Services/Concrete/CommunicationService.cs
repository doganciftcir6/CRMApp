using AutoMapper;
using FluentValidation;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.Repositories.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.CommunicationDtos;
using Onicorn.CRMApp.Dtos.TokenDtos;
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
        private readonly IValidator<CommunicationCreateDto> _communicationCreateDtoValidator;
        public CommunicationService(IUow uow, IMapper mapper, ICommunicationRepository communicationRepository, IValidator<CommunicationCreateDto> communicationCreateDtoValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _communicationRepository = communicationRepository;
            _communicationCreateDtoValidator = communicationCreateDtoValidator;
        }

        public async Task<CustomResponse<IEnumerable<CommunicationDto>>> GetCommunicatiosAsync()
        {
            var communicationDto = _mapper.Map<IEnumerable<CommunicationDto>>(await _communicationRepository.GetAllAsync());
            return CustomResponse<IEnumerable<CommunicationDto>>.Success(communicationDto, ResponseStatusCode.OK);
        }

        public async Task<CustomResponse<NoContent>> InsertCommunicationAsync(CommunicationCreateDto communicationCreateDto)
        {
            var validationResult = _communicationCreateDtoValidator.Validate(communicationCreateDto);
            if (validationResult.IsValid)
            {
                Communication communication = _mapper.Map<Communication>(communicationCreateDto);
                communication.InsertTime = DateTime.UtcNow;
                communication.Status = true;
                await _uow.GetRepository<Communication>().InsertAsync(communication);
                await _uow.SaveChangesAsync();
                return CustomResponse<NoContent>.Success(ResponseStatusCode.CREATED);
            }
            return CustomResponse<NoContent>.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), ResponseStatusCode.BAD_REQUEST);
        }
    }
}
