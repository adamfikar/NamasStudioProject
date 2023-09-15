using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NamasStudio.Dto;
using NamasStudio.Dto.Category;
using NamasStudio.Services.Services.Interfaces;
using NamasStudio.Web.MVC.Models.Category;
using System.Text;
using System.Text.Json;

namespace NamasStudio.Web.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryProductService _service;

        public CategoryController(ICategoryProductService service)
        {
            _service = service;
        }

        [HttpGet("CategoryIndex")]
        public IActionResult Index([FromQuery] int pageSize = 10,
                                   [FromQuery] int pageNumber = 1,
                                   [FromQuery] string category = "")
        {

            var data = _service.GetCategory(pageSize, pageNumber, category);
            if (data.TotalPages < pageNumber)
            {
                data = _service.GetCategory(pageSize, 1, category);
            }
            var viewModel = new CategoryGridViewModel { Dto = data, Category = category };
            return View(viewModel);
        }
    }
}

