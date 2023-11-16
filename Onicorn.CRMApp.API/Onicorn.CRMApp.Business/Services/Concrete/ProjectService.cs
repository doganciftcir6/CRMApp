using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Onicorn.CRMApp.Business.Helpers.UploadHelpers;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Business.ValidationRules.FluentValidation.CustomerValidations;
using Onicorn.CRMApp.DataAccess.Repositories.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.AppUserDtos;
using Onicorn.CRMApp.Dtos.CustomerDtos;
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
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IValidator<ProjectCreateDto> _projectCreateDtoValidator;
        public ProjectService(IMapper mapper, IUow uow, IProjectRepository projectRepository, IValidator<ProjectCreateDto> projectCreateDtoValidator, IHostingEnvironment hostingEnvironment)
        {
            _mapper = mapper;
            _uow = uow;
            _projectRepository = projectRepository;
            _projectCreateDtoValidator = projectCreateDtoValidator;
            _hostingEnvironment = hostingEnvironment;
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

        public async Task<CustomResponse<NoContent>> InsertProjectAsync(ProjectCreateDto projectCreateDto, CancellationToken cancellationToken)
        {
            var validationResult = _projectCreateDtoValidator.Validate(projectCreateDto);
            if (validationResult.IsValid)
            {
                Project project = _mapper.Map<Project>(projectCreateDto);
                if (projectCreateDto.ImageURL != null && projectCreateDto.ImageURL.Length > 0)
                {
                    await ProjectImageUploadHelper.Run(_hostingEnvironment, projectCreateDto.ImageURL, cancellationToken);
                }
                project.InsertTime = DateTime.UtcNow;
                project.Status = true;
                project.ImageURL = Path.GetFileNameWithoutExtension(projectCreateDto.ImageURL.FileName) + Guid.NewGuid().ToString("N") + Path.GetExtension(projectCreateDto.ImageURL.FileName);
                await _uow.GetRepository<Project>().InsertAsync(project);
                await _uow.SaveChangesAsync();
                return CustomResponse<NoContent>.Success(ResponseStatusCode.CREATED);
            }
            return CustomResponse<NoContent>.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), ResponseStatusCode.BAD_REQUEST);
        }
    }
}
