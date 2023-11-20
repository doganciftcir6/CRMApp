using AutoMapper;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.TaskSituationDtos;
using Onicorn.CRMApp.Entities;
using Onicorn.CRMApp.Shared.Utilities.Response;

namespace Onicorn.CRMApp.Business.Services.Concrete
{
    public class TaskSituationService : ITaskSituationService
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        public TaskSituationService(IMapper mapper, IUow uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<CustomResponse<IEnumerable<TaskSituationsDto>>> GetTaskSituationsAsync()
        {
            IEnumerable<TaskSituationsDto> taskSituationsDto = _mapper.Map<IEnumerable<TaskSituationsDto>>(await _uow.GetRepository<TaskSituation>().GetAllAsync());
            return CustomResponse<IEnumerable<TaskSituationsDto>>.Success(taskSituationsDto, ResponseStatusCode.OK);
        }
    }
}
