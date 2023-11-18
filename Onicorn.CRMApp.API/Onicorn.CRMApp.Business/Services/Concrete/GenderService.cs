using AutoMapper;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.GenderDtos;
using Onicorn.CRMApp.Entities;
using Onicorn.CRMApp.Shared.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Services.Concrete
{
    public class GenderService : IGenderService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public GenderService(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CustomResponse<IEnumerable<GenderDto>>> GetGendersAsync()
        {
            IEnumerable<GenderDto> genderDto = _mapper.Map<IEnumerable<GenderDto>>(await _uow.GetRepository<Gender>().GetAllAsync());
            return CustomResponse<IEnumerable<GenderDto>>.Success(genderDto, ResponseStatusCode.OK);
        }
    }
}
