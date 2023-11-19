using AutoMapper;
using FluentValidation;
using Onicorn.CRMApp.Business.Helpers.Messages;
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
        private readonly IValidator<SaleUpdateDto> _saleUpdateDtoValidator;
        public SaleService(IUow uow, ISaleRepository saleRepository, IMapper mapper, IValidator<SaleCreateDto> saleCreateDtoValidator, IValidator<SaleUpdateDto> saleUpdateDtoValidator)
        {
            _uow = uow;
            _saleRepository = saleRepository;
            _mapper = mapper;
            _saleCreateDtoValidator = saleCreateDtoValidator;
            _saleUpdateDtoValidator = saleUpdateDtoValidator;
        }

        public async Task<CustomResponse<SalesDto>> GetSaleByIdAsync(int saleId)
        {
            SalesDto salesDto = _mapper.Map<SalesDto>(await _saleRepository.GetByFilterAsync(x => x.Status == true && x.Id == saleId));
            if (salesDto is not null)
            {
                return CustomResponse<SalesDto>.Success(salesDto, ResponseStatusCode.OK);

            }
            return CustomResponse<SalesDto>.Fail(SaleMessages.NOT_FOUND_SALE, ResponseStatusCode.NOT_FOUND);
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

        public async Task<CustomResponse<NoContent>> RemoveSaleAsync(int saleId)
        {
            Sale sale = await _uow.GetRepository<Sale>().GetByIdAsync(saleId);
            if (sale != null)
            {
                sale.Status = false;
                _uow.GetRepository<Sale>().Update(sale);
                await _uow.SaveChangesAsync();
                return CustomResponse<NoContent>.Success(ResponseStatusCode.OK);
            }
            return CustomResponse<NoContent>.Fail(SaleMessages.NOT_FOUND_SALE, ResponseStatusCode.NOT_FOUND);
        }

        public async Task<CustomResponse<NoContent>> UpdateSaleAsync(SaleUpdateDto saleUpdateDto)
        {
            var validationResult = _saleUpdateDtoValidator.Validate(saleUpdateDto);
            if (validationResult.IsValid)
            {
                Sale oldData = await _uow.GetRepository<Sale>().AsNoTrackingGetByFilterAsync(x => x.Id == saleUpdateDto.Id);
                if (oldData == null)
                    return CustomResponse<NoContent>.Fail(SaleMessages.NOT_FOUND_SALE, ResponseStatusCode.NOT_FOUND);

                if (saleUpdateDto.SalesDate == null)
                    saleUpdateDto.SalesDate = oldData.SalesDate;
                Sale sale = _mapper.Map<Sale>(saleUpdateDto);

                _uow.GetRepository<Sale>().Update(sale);
                await _uow.SaveChangesAsync();
                return CustomResponse<NoContent>.Success(ResponseStatusCode.OK);
            }
            return CustomResponse<NoContent>.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), ResponseStatusCode.BAD_REQUEST);
        }
    }
}
