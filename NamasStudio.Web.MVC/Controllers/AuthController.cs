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
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                var model = new LoginViewModel
                {
                    Dto = dto
                };
                return View(model);
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
            var model = new LoginViewModel();
            return View(model);
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
