using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Onicorn.CRMApp.Business.Helpers.Messages;
using Onicorn.CRMApp.Business.Helpers.UploadHelpers;
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
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IValidator<ProjectCreateDto> _projectCreateDtoValidator;
        private readonly IValidator<ProjectUpdateDto> _projectUpdateDtoValidator;
        public ProjectService(IMapper mapper, IUow uow, IProjectRepository projectRepository, IValidator<ProjectCreateDto> projectCreateDtoValidator, IHostingEnvironment hostingEnvironment, IValidator<ProjectUpdateDto> projectUpdateDtoValidator)
        {
            _mapper = mapper;
            _uow = uow;
            _projectRepository = projectRepository;
            _projectCreateDtoValidator = projectCreateDtoValidator;
            _hostingEnvironment = hostingEnvironment;
            _projectUpdateDtoValidator = projectUpdateDtoValidator;
        }

        public async Task<CustomResponse<ProjectDto>> GetProjectByIdAsync(int projectId)
        {
            Project project = await _uow.GetRepository<Project>().GetByIdAsync(projectId);
            if (project != null)
            {
                ProjectDto projectDto = _mapper.Map<ProjectDto>(project);
                return CustomResponse<ProjectDto>.Success(projectDto, ResponseStatusCode.OK);
            }
            return CustomResponse<ProjectDto>.Fail(ProjectMessages.NOT_FOUND_PROJECT, ResponseStatusCode.NOT_FOUND);
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
                    string createdFileName = await ProjectImageUploadHelper.Run(_hostingEnvironment, projectCreateDto.ImageURL, cancellationToken);
                    project.ImageURL = createdFileName;
                }
                project.InsertTime = DateTime.UtcNow;
                project.Status = true;
                await _uow.GetRepository<Project>().InsertAsync(project);
                await _uow.SaveChangesAsync();
                return CustomResponse<NoContent>.Success(ResponseStatusCode.CREATED);
            }
            return CustomResponse<NoContent>.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), ResponseStatusCode.BAD_REQUEST);
        }

        public async Task<CustomResponse<NoContent>> RemoveProjectAsync(int projectId)
        {
            Project project = await _uow.GetRepository<Project>().GetByIdAsync(projectId);
            if (project != null)
            {
                project.Status = false;
                _uow.GetRepository<Project>().Update(project);
                if (project.ImageURL != null && project.ImageURL.Length != 0)
                {
                    ProjectImageDeleteHelper.Delete(_hostingEnvironment, project.ImageURL);
                    project.ImageURL = null;
                }
                await _uow.SaveChangesAsync();
                return CustomResponse<NoContent>.Success(ResponseStatusCode.OK);
            }
            return CustomResponse<NoContent>.Fail(ProjectMessages.NOT_FOUND_PROJECT, ResponseStatusCode.NOT_FOUND);
        }

        public async Task<CustomResponse<NoContent>> UpdateProjectAsync(ProjectUpdateDto projectUpdateDto, CancellationToken cancellationToken)
        {
            var validationResult = _projectUpdateDtoValidator.Validate(projectUpdateDto);
            if (validationResult.IsValid)
            {
                Project oldData = await _uow.GetRepository<Project>().AsNoTrackingGetByFilterAsync(x => x.Id == projectUpdateDto.Id);
                if (oldData == null)
                    return CustomResponse<NoContent>.Fail(ProjectMessages.NOT_FOUND_PROJECT, ResponseStatusCode.NOT_FOUND);

                Project project = _mapper.Map<Project>(projectUpdateDto);
                if (projectUpdateDto.ImageURL != null && projectUpdateDto.ImageURL.Length > 0)
                {
                    string createdFileName = await ProjectImageUploadHelper.Run(_hostingEnvironment, projectUpdateDto.ImageURL, cancellationToken);
                    project.ImageURL = createdFileName;
                }
                else
                {
                    project.ImageURL = oldData.ImageURL;
                }
                project.UpdateTime = DateTime.UtcNow;
                project.InsertTime = oldData.InsertTime;
                _uow.GetRepository<Project>().Update(project);
                await _uow.SaveChangesAsync();
                return CustomResponse<NoContent>.Success(ResponseStatusCode.OK);
            }
            return CustomResponse<NoContent>.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), ResponseStatusCode.BAD_REQUEST);
        }
    }
}
