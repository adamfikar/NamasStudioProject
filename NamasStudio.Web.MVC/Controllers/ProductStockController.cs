using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NamasStudio.Services.Services.Interfaces;
using NamasStudio.Web.MVC.Models.ProductStock;

namespace NamasStudio.Web.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductStockController : Controller
    {
        private readonly IProductStockService _service;

        public ProductStockController(IProductStockService service)
        {
            _service = service;
        }

        [HttpGet("StockIndex")]
        public IActionResult StockIndex([FromQuery] int productId,
                                        [FromQuery] int pageSize = 10,
                                        [FromQuery] int pageNumber = 1)
        {

            var data = _service.GetAllStockProductById(pageSize, pageNumber, productId);
            if (data.TotalPages < pageNumber)
            {
                data = _service.GetAllStockProductById(pageSize, 1, productId);
            }
            var productName = _service.GetProductById(productId);
            var viewModel = new ProductStockGridViewModel { Dto = data, Product = productName };

            return View(viewModel);
        }
    }
}
