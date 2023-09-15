using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NamasStudio.Services.Services.Interfaces;
using NamasStudio.Web.MVC.Models.ProductSize;

namespace NamasStudio.Web.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductSizeController : Controller
    {
        private readonly IProductSizeService _service;

        public ProductSizeController(IProductSizeService service)
        {
            _service = service;
        }

        [HttpGet("ProductSizeIndex")]
        public IActionResult Index([FromQuery] int pageSize = 10,
                                   [FromQuery] int pageNumber = 1,
                                   [FromQuery] string sizeName = "")
        {

            var data = _service.GetProductSize(pageSize, pageNumber,sizeName);
            if (data.TotalPages < pageNumber)
            {
                data = _service.GetProductSize(pageSize, 1,sizeName);
            }
            var viewModel = new ProductSizeGridViewModel { Dto = data , Size = sizeName};
            return View(viewModel);
        }
    }
}
