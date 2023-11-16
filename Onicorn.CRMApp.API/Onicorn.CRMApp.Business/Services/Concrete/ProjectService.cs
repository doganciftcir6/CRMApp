using AutoMapper;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.Repositories.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.ProjectDtos;
using Onicorn.CRMApp.Entities;
using Onicorn.CRMApp.Shared.Utilities.Response;

namespace Onicorn.CRMApp.Business.Services.Concrete
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IMapper mapper, IUow uow, IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _uow = uow;
            _projectRepository = projectRepository;
        }

        public async Task<CustomResponse<ProjectDto>> GetProjectAsync(int projectId)
        {
            Project project = await _uow.GetRepository<Project>().GetByIdAsync(projectId);
            if (project != null)
            {
                ProjectDto projectDto = _mapper.Map<ProjectDto>(project);
                return CustomResponse<ProjectDto>.Success(projectDto, ResponseStatusCode.OK);
            }
            return CustomResponse<ProjectDto>.Fail("Project not found", ResponseStatusCode.NOT_FOUND);
        }

        public async Task<CustomResponse<IEnumerable<ProjectsDto>>> GetProjectsAsync()
        {
            IEnumerable<ProjectsDto> projectsDto = _mapper.Map<IEnumerable<ProjectsDto>>(await _projectRepository.GetAllFilterAsync(x => x.Status == true));
            return CustomResponse<IEnumerable<ProjectsDto>>.Success(projectsDto, ResponseStatusCode.OK);
        }
    }
}
