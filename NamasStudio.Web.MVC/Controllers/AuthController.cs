using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NamasStudio.Dto.Auth;
using NamasStudio.Services.Services.Interfaces;
using NamasStudio.Web.MVC.Models.Auth;

namespace NamasStudio.Web.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new AllAuthViewModel
            {
                Login = new LoginViewModel(),
                Register = new RegisterViewModel()
            };

            return View(model);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }
            var authTicket = _service.GetAuthenticationTicket(dto);
            await HttpContext.SignInAsync(authTicket.AuthenticationScheme,
                                          authTicket.Principal,
                                          authTicket.Properties);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            var model = new AllAuthViewModel
            {
                Login = new LoginViewModel(),
                Register = new RegisterViewModel()
            };

            return View(model);
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View("Register");
            }
            _service.RegisterUser(dto);
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to the home page or wherever you want after logout
            return RedirectToAction("Login");
        }


    }
}
