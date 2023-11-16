using AutoMapper;
using FluentValidation;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.CustomerDtos;
using Onicorn.CRMApp.Entities;
using Onicorn.CRMApp.Shared.Utilities.Response;

namespace Onicorn.CRMApp.Business.Services.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CustomerCreateDto> _customerCreateDtoValidator;
        private readonly IValidator<CustomerUpdateDto> _customerUpdateDtoValidator;
        public CustomerService(IUow uow, IMapper mapper, IValidator<CustomerCreateDto> customerCreateDtoValidator, IValidator<CustomerUpdateDto> customerUpdateDtoValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _customerCreateDtoValidator = customerCreateDtoValidator;
            _customerUpdateDtoValidator = customerUpdateDtoValidator;
        }

        public async Task<CustomResponse<CustomerDto>> GetCustomerAsync(int customerId)
        {
            Customer customer = await _uow.GetRepository<Customer>().GetByIdAsync(customerId);
            if (customer != null)
            {
                CustomerDto customerDto = _mapper.Map<CustomerDto>(customer);
                return CustomResponse<CustomerDto>.Success(customerDto, ResponseStatusCode.OK);
            }
            return CustomResponse<CustomerDto>.Fail("Customer not found", ResponseStatusCode.NOT_FOUND);
        }

        public async Task<CustomResponse<IEnumerable<CustomersDto>>> GetCustomersAsync()
        {
            var customerDto = _mapper.Map<IEnumerable<CustomersDto>>(await _uow.GetRepository<Customer>().GetAllFilterAsync(x => x.Status == true));
            return CustomResponse<IEnumerable<CustomersDto>>.Success(customerDto, ResponseStatusCode.OK);
        }

        public async Task<CustomResponse<NoContent>> InsertCustomerAsync(CustomerCreateDto customerCreateDto)
        {
            var validationResult = _customerCreateDtoValidator.Validate(customerCreateDto);
            if (validationResult.IsValid)
            {
                Customer customer = _mapper.Map<Customer>(customerCreateDto);
                customer.InsertTime = DateTime.UtcNow;
                customer.Status = true;
                await _uow.GetRepository<Customer>().InsertAsync(customer);
                await _uow.SaveChangesAsync();
                return CustomResponse<NoContent>.Success(ResponseStatusCode.CREATED);
            }
            return CustomResponse<NoContent>.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), ResponseStatusCode.BAD_REQUEST);
        }

        public async Task<CustomResponse<NoContent>> RemoveCustomerAsync(int customerId)
        {
            Customer customer = await _uow.GetRepository<Customer>().GetByIdAsync(customerId);
            if (customer != null)
            {
                customer.Status = false;
                _uow.GetRepository<Customer>().Update(customer);
                await _uow.SaveChangesAsync();
                return CustomResponse<NoContent>.Success(ResponseStatusCode.OK);
            }
            return CustomResponse<NoContent>.Fail("Customer not found", ResponseStatusCode.NOT_FOUND);
        }

        public async Task<CustomResponse<NoContent>> UpdateCustomerAsync(CustomerUpdateDto customerUpdateDto)
        {
            var validationResult = _customerUpdateDtoValidator.Validate(customerUpdateDto);
            if (validationResult.IsValid)
            {
                Customer oldData = await _uow.GetRepository<Customer>().AsNoTrackingGetByFilterAsync(x => x.Id == customerUpdateDto.Id);
                if (oldData == null)
                    return CustomResponse<NoContent>.Fail("Customer not found", ResponseStatusCode.NOT_FOUND);

                Customer customer = _mapper.Map<Customer>(customerUpdateDto);
                customer.UpdateTime = DateTime.UtcNow;
                customer.InsertTime = oldData.InsertTime;
                _uow.GetRepository<Customer>().Update(customer);
                await _uow.SaveChangesAsync();
                return CustomResponse<NoContent>.Success(ResponseStatusCode.OK);
            }
            return CustomResponse<NoContent>.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), ResponseStatusCode.BAD_REQUEST);
        }
    }
}
