using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.Dtos.AppUserDtos;
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
        public AppUserService(UserManager<AppUser> userManager, ISharedIdentityService sharedIdentityService, IMapper mapper)
        {
            _userManager = userManager;
            _sharedIdentityService = sharedIdentityService;
            _mapper = mapper;
        }

        public async Task<CustomResponse<AppUserDto>> GetProfileAsync()
        {
            var userInfo = await _userManager.Users.Include(u => u.Gender).FirstOrDefaultAsync(u => u.Id == int.Parse(_sharedIdentityService.GetUserId));
            if (userInfo != null)
            {
                var appUserDto = _mapper.Map<AppUserDto>(userInfo);
                return CustomResponse<AppUserDto>.Success(appUserDto, ResponseStatusCode.OK);
            }
            return CustomResponse<AppUserDto>.Fail("User not found!", ResponseStatusCode.NOT_FOUND);
        }

        public Task<CustomResponse<AppUserDto>> UpdateProfileAsync(UpdateAppUserDto updateAppUserDto)
        {
            throw new NotImplementedException();
        }
    }
}
