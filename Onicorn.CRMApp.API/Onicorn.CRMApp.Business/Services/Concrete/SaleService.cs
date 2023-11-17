using AutoMapper;
using FluentValidation;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.Repositories.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.SaleDtos;
using Onicorn.CRMApp.Entities;
using Onicorn.CRMApp.Shared.Utilities.Response;

namespace Onicorn.CRMApp.Business.Services.Concrete
{
    public class SaleService : ISaleService
    {
        private readonly IUow _uow;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<SaleCreateDto> _saleCreateDtoValidator;
        public SaleService(IUow uow, ISaleRepository saleRepository, IMapper mapper, IValidator<SaleCreateDto> saleCreateDtoValidator)
        {
            _uow = uow;
            _saleRepository = saleRepository;
            _mapper = mapper;
            _saleCreateDtoValidator = saleCreateDtoValidator;
        }

        public async Task<CustomResponse<IEnumerable<SalesDto>>> GetSalesAsync()
        {
            IEnumerable<SalesDto> salesDtos = _mapper.Map<IEnumerable<SalesDto>>(await _saleRepository.GetAllFilterAsync(x => x.Status == true));
            return CustomResponse<IEnumerable<SalesDto>>.Success(salesDtos, ResponseStatusCode.OK);
        }

        public async Task<CustomResponse<NoContent>> InsertSaleAsync(SaleCreateDto saleCreateDto)
        {
            var validationResult = _saleCreateDtoValidator.Validate(saleCreateDto);
            if (validationResult.IsValid)
            {
                Sale sale = _mapper.Map<Sale>(saleCreateDto);
                sale.Status = true;
                await _uow.GetRepository<Sale>().InsertAsync(sale);
                await _uow.SaveChangesAsync();
                return CustomResponse<NoContent>.Success(ResponseStatusCode.OK);
            }
            return CustomResponse<NoContent>.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), ResponseStatusCode.BAD_REQUEST);
        }
    }
}
