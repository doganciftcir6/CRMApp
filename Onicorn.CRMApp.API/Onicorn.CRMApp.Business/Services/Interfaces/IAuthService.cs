using Onicorn.CRMApp.Dtos.AppUserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Business.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterWithRoleAsync(AppUserRegisterDto appUserRegisterDto);
    }
}
