using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace NamasStudio.Web.MVC.Controllers
{
    [Authorize]
    public class ErrorsController : Controller
    {
        [Route("Auth/AccessDenied")]
        public IActionResult PageNeedAuthorize()
        {
            return View();
        }
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
