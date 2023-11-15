using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Dtos.AppUserDtos;
using Onicorn.CRMApp.Dtos.TokenDtos;
using Onicorn.CRMApp.Entities;
using Onicorn.CRMApp.Shared.Utilities.Response;
using Onicorn.CRMApp.Shared.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Services.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateAppUserDto> _updateAppUserValidator;
        public AppUserService(UserManager<AppUser> userManager, ISharedIdentityService sharedIdentityService, IMapper mapper, IValidator<UpdateAppUserDto> updateAppUserValidator)
        {
            _userManager = userManager;
            _sharedIdentityService = sharedIdentityService;
            _mapper = mapper;
            _updateAppUserValidator = updateAppUserValidator;
        }

        public async Task<CustomResponse<AppUserDto>> GetProfileAsync()
        {
            var userInfo = await _userManager.Users.Include(u => u.Gender).FirstOrDefaultAsync(u => u.Id == _sharedIdentityService.GetUserId);
            if (userInfo != null)
            {
                var appUserDto = _mapper.Map<AppUserDto>(userInfo);
                return CustomResponse<AppUserDto>.Success(appUserDto, ResponseStatusCode.OK);
            }
            return CustomResponse<AppUserDto>.Fail("User not found!", ResponseStatusCode.NOT_FOUND);
        }

        public async Task<CustomResponse<NoContent>> UpdateProfileAsync(UpdateAppUserDto updateAppUserDto)
        {
            var validationResult = _updateAppUserValidator.Validate(updateAppUserDto);
            if (validationResult.IsValid)
            {
                var appUser = await _userManager.FindByIdAsync(_sharedIdentityService.GetUserId.ToString());
                if (appUser != null)
                {
                    _mapper.Map(updateAppUserDto, appUser);

                    var updateResult = await _userManager.UpdateAsync(appUser);

                    if (updateResult.Succeeded)
                    {
                        return CustomResponse<NoContent>.Success(ResponseStatusCode.OK);
                    }
                    return CustomResponse<NoContent>.Fail(updateResult.Errors.Select(x => x.Description).ToList(), ResponseStatusCode.BAD_REQUEST);
                }
                return CustomResponse<NoContent>.Fail("User not found!", ResponseStatusCode.NOT_FOUND);
            }
            return CustomResponse<NoContent>.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), ResponseStatusCode.BAD_REQUEST);
        }
    }
}
