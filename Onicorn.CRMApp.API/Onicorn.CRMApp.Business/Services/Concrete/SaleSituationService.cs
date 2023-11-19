using AutoMapper;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.SaleSituationDtos;
using Onicorn.CRMApp.Entities;
using Onicorn.CRMApp.Shared.Utilities.Response;

namespace Onicorn.CRMApp.Business.Services.Concrete
{
    public class SaleSituationService : ISaleSituationService
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        public SaleSituationService(IMapper mapper, IUow uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<CustomResponse<IEnumerable<SaleSituationsDto>>> GetSaleSituationsAsync()
        {
            IEnumerable<SaleSituationsDto> saleSituationsDto = _mapper.Map<IEnumerable<SaleSituationsDto>>(await _uow.GetRepository<SaleSituation>().GetAllAsync());
            return CustomResponse<IEnumerable<SaleSituationsDto>>.Success(saleSituationsDto, ResponseStatusCode.OK);
        }
    }
}
