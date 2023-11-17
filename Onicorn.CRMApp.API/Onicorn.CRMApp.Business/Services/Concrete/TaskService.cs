using AutoMapper;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.Repositories.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.TaskDtos;
using Onicorn.CRMApp.Shared.Utilities.Response;
using Onicorn.CRMApp.Shared.Utilities.Services;

namespace Onicorn.CRMApp.Business.Services.Concrete
{
    public class TaskService : ITaskService
    {
        private readonly IUow _uow;
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly ISharedIdentityService _sharedIdentityService;
        public TaskService(IUow uow, ITaskRepository taskRepository, IMapper mapper, ISharedIdentityService sharedIdentityService)
        {
            _uow = uow;
            _taskRepository = taskRepository;
            _mapper = mapper;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<CustomResponse<IEnumerable<TasksDto>>> GetTasks()
        {
            IEnumerable<TasksDto> tasksDtos = _mapper.Map<IEnumerable<TasksDto>>(await _taskRepository.GetAllFilterAsync(x => x.Status == true));
            return CustomResponse<IEnumerable<TasksDto>>.Success(tasksDtos, ResponseStatusCode.OK);
        }

        public async Task<CustomResponse<IEnumerable<TasksDto>>> GetTasksByUser()
        {
            IEnumerable<TasksDto> tasksDtos = _mapper.Map<IEnumerable<TasksDto>>(await _taskRepository.GetAllFilterAsync(x => x.Status == true && x.AppUserId == _sharedIdentityService.GetUserId));
            return CustomResponse<IEnumerable<TasksDto>>.Success(tasksDtos, ResponseStatusCode.OK);
        }
    }
}
