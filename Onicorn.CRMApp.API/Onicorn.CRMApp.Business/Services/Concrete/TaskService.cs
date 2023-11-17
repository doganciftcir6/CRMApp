using AutoMapper;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.Repositories.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.TaskDtos;
using Onicorn.CRMApp.Shared.Utilities.Response;

namespace Onicorn.CRMApp.Business.Services.Concrete
{
    public class TaskService : ITaskService
    {
        private readonly IUow _uow;
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public TaskService(IUow uow, ITaskRepository taskRepository, IMapper mapper)
        {
            _uow = uow;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponse<IEnumerable<TasksDto>>> GetTasks()
        {
            IEnumerable<TasksDto> tasksDtos = _mapper.Map<IEnumerable<TasksDto>>(await _taskRepository.GetAllFilterAsync(x => x.Status == true));
            return CustomResponse<IEnumerable<TasksDto>>.Success(tasksDtos, ResponseStatusCode.OK);
        }
    }
}
