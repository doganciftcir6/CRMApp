using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IValidator<AppUserRegisterDto> _AppUserRegisterDtoValidator;
        private readonly IValidator<AppUserLoginDto> _AppUserLoginDtoValidator;
        private readonly IMapper _mapper;
        public AuthService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IValidator<AppUserRegisterDto> appUserRegisterDtoValidator, IValidator<AppUserLoginDto> appUserLoginDtoValidator, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _AppUserRegisterDtoValidator = appUserRegisterDtoValidator;
            _AppUserLoginDtoValidator = appUserLoginDtoValidator;
            _mapper = mapper;
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

        public async Task<CustomResponse<string>> RegisterWithRoleAsync(AppUserRegisterDto appUserRegisterDto)
        {
            var validationResult = _AppUserRegisterDtoValidator.Validate(appUserRegisterDto);
            if (validationResult.IsValid)
            {
                var appUser = _mapper.Map<AppUser>(appUserRegisterDto);
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
