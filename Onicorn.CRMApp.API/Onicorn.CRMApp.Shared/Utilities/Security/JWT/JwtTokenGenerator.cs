using Microsoft.IdentityModel.Tokens;
using Onicorn.CRMApp.Dtos.AppUserDtos;
using Onicorn.CRMApp.Dtos.TokenDtos;
using Onicorn.CRMApp.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Shared.Utilities.Security.JWT
{
    public static class JwtTokenGenerator
    {
        public static TokenResponseDto GenerateToken(AppUserDto appUserDto, IList<string> roles)
        {
            //Security Key'in simetriğini alalım
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));
            //ExpireDate oluşturalım (token geçerlilik süresi)
            var expireDate = DateTime.UtcNow.AddMinutes(5);
            //Şifrelenmiş kimliği oluşturuyoruz
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //token oluşurken token bilgisinin içerisinde kullanıcının rolü ve adı ve nameIdentifier ve email de olsun.
            List<Claim> myClaims = new List<Claim>();
            if (roles.Count > 0)
            {
                foreach (var claim in roles)
                {
                    myClaims.Add(new Claim(ClaimTypes.Role, claim));
                }
            }
            if (appUserDto.Id != null)
            {
                myClaims.Add(new Claim(ClaimTypes.NameIdentifier, appUserDto.Id.ToString()));
            }
            if (!string.IsNullOrEmpty(appUserDto.Username))
            {
                myClaims.Add(new Claim(ClaimTypes.Name, appUserDto.Username));
            }
            if (!string.IsNullOrEmpty(appUserDto.Email))
            {
                myClaims.Add(new Claim("Email", appUserDto.Email));
            }
            //Token ayarlarını yapıyoruz
            JwtSecurityToken token = new JwtSecurityToken(issuer: JwtTokenDefaults.ValidIssuer, audience: JwtTokenDefaults.ValidAudience, claims: myClaims, notBefore: DateTime.UtcNow, expires: expireDate, signingCredentials: credentials);

            //Token oluşturucu sınıfından bir örnek alalım
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            //Token üretelim
            //return handler.WriteToken(token);
            return new TokenResponseDto(handler.WriteToken(token), expireDate);
        }
    }
}
