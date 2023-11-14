using Microsoft.AspNetCore.Identity;
using Onicorn.CRMApp.Business.Services.Interfaces;
using Onicorn.CRMApp.DataAccess.UnitOfWork;
using Onicorn.CRMApp.Dtos.AppUserDtos;
using Onicorn.CRMApp.Entities;
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
        public AuthService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> RegisterWithRoleAsync(AppUserRegisterDto appUserRegisterDto)
        {
            AppUser appUser = new AppUser()
            {
                Firstname = appUserRegisterDto.Firstname,
                Lastname = appUserRegisterDto.Lastname,
                Email = appUserRegisterDto.Email,
                UserName = appUserRegisterDto.Username,
                ImageURL = appUserRegisterDto.ImageURL,
                GenderId = appUserRegisterDto.GenderId,
            };
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
                return true;
            }
            return false;
        }
    }
}
