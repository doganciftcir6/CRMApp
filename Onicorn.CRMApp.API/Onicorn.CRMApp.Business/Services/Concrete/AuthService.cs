using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Onicorn.CRMApp.Business.Helpers.UploadHelpers;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.AppUserDtos;
using Onicorn.CRMApp.Dtos.TokenDtos;
using Onicorn.CRMApp.Entities;
using Onicorn.CRMApp.Shared.Utilities.Response;
using Onicorn.CRMApp.Shared.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IValidator<AppUserRegisterDto> _AppUserRegisterDtoValidator;
        private readonly IValidator<AppUserLoginDto> _AppUserLoginDtoValidator;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AuthService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IValidator<AppUserRegisterDto> appUserRegisterDtoValidator, IValidator<AppUserLoginDto> appUserLoginDtoValidator, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _AppUserRegisterDtoValidator = appUserRegisterDtoValidator;
            _AppUserLoginDtoValidator = appUserLoginDtoValidator;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<CustomResponse<TokenResponseDto>> LoginAsync(AppUserLoginDto appUserLoginDto)
        {
            var validationResult = _AppUserLoginDtoValidator.Validate(appUserLoginDto);
            if (validationResult.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(appUserLoginDto.UserName);
                if (user != null)
                {
                    var checkPassword = await _userManager.CheckPasswordAsync(user, appUserLoginDto.Password);
                    if (checkPassword)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        AppUserDto appUserDto = _mapper.Map<AppUserDto>(user);
                        var token = JwtTokenGenerator.GenerateToken(appUserDto, roles);
                        return CustomResponse<TokenResponseDto>.Success(token, ResponseStatusCode.OK);
                    }
                }
                return CustomResponse<TokenResponseDto>.Fail("Email or password is incorrect", ResponseStatusCode.BAD_REQUEST);
            }
            return CustomResponse<TokenResponseDto>.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), ResponseStatusCode.BAD_REQUEST);
        }

        public async Task<CustomResponse<string>> RegisterWithRoleAsync(AppUserRegisterDto appUserRegisterDto, CancellationToken cancellationToken)
        {
            var validationResult = _AppUserRegisterDtoValidator.Validate(appUserRegisterDto);
            if (validationResult.IsValid)
            {
                if(appUserRegisterDto.ImageURL != null && appUserRegisterDto.ImageURL.Length > 0)
                {
                    await AppUserImageUploadHelper.Run(_hostingEnvironment, appUserRegisterDto.ImageURL, cancellationToken);
                }

                var appUser = _mapper.Map<AppUser>(appUserRegisterDto);
                appUser.ImageURL = Path.GetFileNameWithoutExtension(appUserRegisterDto.ImageURL.FileName) + Guid.NewGuid().ToString("N") + Path.GetExtension(appUserRegisterDto.ImageURL.FileName);
                var registerResult = await _userManager.CreateAsync(appUser, appUserRegisterDto.Password);
                if (registerResult.Succeeded)
                {
                    var memberRole = await _roleManager.FindByNameAsync("Member");
                    if (memberRole == null)
                    {
                        //eğer db'de Member rolü daha önce yoksa oluştursun
                        await _roleManager.CreateAsync(new()
                        {
                            Name = "Member",
                        });
                    }
                    //register olan kullanıcıya default member rolünü ekle
                    await _userManager.AddToRoleAsync(appUser, "Member");
                    return CustomResponse<string>.Success("User has been successfully created.", ResponseStatusCode.CREATED);
                }
                return CustomResponse<string>.Fail(registerResult.Errors.Select(x => x.Description).ToList(), ResponseStatusCode.BAD_REQUEST);
            }
            return CustomResponse<string>.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), ResponseStatusCode.BAD_REQUEST);
        }
    }
}
