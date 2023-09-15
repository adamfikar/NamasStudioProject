using Microsoft.AspNetCore.Authentication;
using NamasStudio.DataAccess.Models;
using NamasStudio.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Services.Services.Interfaces
{
    public interface IAuthService
    {
        AuthenticationTicket GetAuthenticationTicket(LoginDto dto);
        string Login(LoginDto dto);
        public User RegisterUser(RegisterDto dto);
    }
}
