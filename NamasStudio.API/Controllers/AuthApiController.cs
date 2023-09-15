using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NamasStudio.Dto.Auth;
using NamasStudio.Services.Services.Interfaces;

namespace NamasStudio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthApiController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public ActionResult<string> LoginUser(LoginDto dto)
        {
            return Ok(_service.Login(dto));
        }
    }
}
