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
        private readonly SignInManager<AppUser> _signInManager;
        public AuthService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<bool> LoginAsync(AppUserLoginDto appUserLoginDto)
        {
            var user = await _userManager.FindByNameAsync(appUserLoginDto.UserName);
            var signInResult = await _signInManager.PasswordSignInAsync(appUserLoginDto.UserName, appUserLoginDto.Password, appUserLoginDto.RememberMe, true);
            if (signInResult.Succeeded)
            {
                return true;
            }
            else if (signInResult.IsLockedOut)
            {
                //ne zamana kadar kilitlendiğinin bilgisini almak yani locked zamanını ayarlamak.
                //13.69 - 14.02 minutes
                var lockOutEnd = await _userManager.GetLockoutEndDateAsync(user);
                var message=  $"Hesabınız {(lockOutEnd.Value.UtcDateTime - DateTime.UtcNow).Minutes} dk süreyle askıya alınmıştır.";
                return false;
            }
            else
            {
                //kaç kere hatalı giriş yapıldığının bilgisini çekelim.
                var message = string.Empty;

                if (user != null)
                {
                    var failedCount = await _userManager.GetAccessFailedCountAsync(user);
                    message = $"{(_userManager.Options.Lockout.MaxFailedAccessAttempts - failedCount)} kez daha girerseniz hesabınız geçici olarak kilitlenecektir";
                }
                else
                {
                    message = "Kullanıcı adı ve şifre hatalı";
                }
                return false;
            }
        }

        public async Task<bool> RegisterWithRoleAsync(AppUserRegisterDto appUserRegisterDto)
        {
            AppUser appUser = new AppUser()
            {
                Firstname = appUserRegisterDto.Firstname,
                Lastname = appUserRegisterDto.Lastname,
                Email = appUserRegisterDto.Email,
                UserName = appUserRegisterDto.Username,
                PhoneNumber = appUserRegisterDto.PhoneNumber,
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
