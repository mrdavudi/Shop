using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application.Auth
{
    public class AuthHelper : IAuthHelper
    {

        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public long CurrentAccountId()
        {
            if(IsAuthenticated())
                return long.Parse(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "AccountId").Value);

            return 0;
        }

        public AuthViewModel CurrentAccountInfo()
        {
            var result = new AuthViewModel();
            if (!IsAuthenticated())
                return result;

            var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            result.Fullname = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            result.Id = long.Parse(claims.FirstOrDefault(x => x.Type == "AccountId").Value);
            result.Username = claims.FirstOrDefault(x => x.Type == "UserName").Value;
            result.RoleId = long.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);
            result.Mobile = claims.FirstOrDefault(x => x.Type == "Mobile").Value;

            return result;
        }

        public string CurrentAccountMobile()
        {
            if (IsAuthenticated())
                return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Mobile").Value;

            return null;
        }

        public string CurrentAccountRole()
        {
            if (IsAuthenticated())
                return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

            return null;
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public void SignIn(AuthViewModel command)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, command.Fullname),
                new Claim("AccountId", command.Id.ToString()),
                new Claim(ClaimTypes.Role, command.RoleId.ToString()),
                new Claim("UserName", command.Username),
                new Claim("Mobile", command.Mobile)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };

            _contextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), 
                authProperties);
        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
