using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NamasStudio.DataAccess.Models;
using NamasStudio.Dto.Auth;
using NamasStudio.Repository.Repositories.Interfaces;
using NamasStudio.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Services.Services.Implementations
{
    internal class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthRepo _userRepo;
        private readonly IConfiguration _configuration;

        public AuthService(IHttpContextAccessor httpContextAccessor, IAuthRepo userRepo, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepo = userRepo;
            _configuration = configuration;
        }

        public User RegisterUser(RegisterDto dto)
        {
            var hash = Argon2.Hash(dto.Password);
            var user = new User
            {
                Username = dto.Username,
                Password = hash,
                RoleId = 1,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                BirthDate = dto.BirthDate,
                RegisterDate = DateTime.Now,

            };
            _userRepo.CreateUser(user);
            return user;

        }

        public AuthenticationTicket GetAuthenticationTicket(LoginDto dto)
        {
            var user = _userRepo.FindUserById(dto.Username);
            if (user.Username != dto.Username)
            {
                throw new Exception("User not found!");
            }
            var password = Argon2.Verify(user.Password, dto.Password);
            if (!password)
            {
                throw new ArgumentException("Password invalid!!");
            }
            ClaimsPrincipal principal = GetPrincipal(user);

            var authProperties = new AuthenticationProperties
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
            };

            var authenticationTicket = new AuthenticationTicket(principal, authProperties, CookieAuthenticationDefaults.AuthenticationScheme);
            var token = CreateToken(user);
            SetCookie(token);
            return authenticationTicket;
        }


        private ClaimsPrincipal GetPrincipal(User user)
        {
            //var token = CreateToken(user);
            var claims = new List<Claim> {
                new Claim("username", user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role.RoleName),
                //new Claim("ApiToken",token)
            };


            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }

        public string Login(LoginDto dto)
        {
            var user = _userRepo.FindUserById(dto.Username);
            if (user.Username != dto.Username)
            {
                throw new Exception("User not found!");
            }

            if (!Argon2.Verify(user.Password, dto.Password))
            {
                throw new Exception("Wrong Password");
            }

            return CreateToken(user);
        }
        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role.RoleName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.
                        GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private void SetCookie(string token)
        {
            // Access the HttpContext from IHttpContextAccessor
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                // Create a new cookie
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(30) // Set an expiration date (optional)
                };

                httpContext.Response.Cookies.Append("token", token, cookieOptions);
            }
        }
    }
}
