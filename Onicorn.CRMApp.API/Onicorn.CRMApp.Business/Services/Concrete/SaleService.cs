using AutoMapper;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.Repositories.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.SaleDtos;
using Onicorn.CRMApp.Shared.Utilities.Response;

namespace Onicorn.CRMApp.Business.Services.Concrete
{
    public class SaleService : ISaleService
    {
        private readonly IUow _uow;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        public SaleService(IUow uow, ISaleRepository saleRepository, IMapper mapper)
        {
            _uow = uow;
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponse<IEnumerable<SalesDto>>> GetSalesAsync()
        {
            IEnumerable<SalesDto> salesDtos = _mapper.Map<IEnumerable<SalesDto>>(await _saleRepository.GetAllFilterAsync(x => x.Status == true));
            return CustomResponse<IEnumerable<SalesDto>>.Success(ResponseStatusCode.OK);
        }
    }
}
