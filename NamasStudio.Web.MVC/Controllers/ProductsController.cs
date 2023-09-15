using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NamasStudio.Dto.Product;
using NamasStudio.Services.Services.Interfaces;
using NamasStudio.Web.MVC.Models.Products;

namespace NamasStudio.Web.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            _service = service;
        }

        public IActionResult Index([FromQuery] int pageSize = 10,
                                   [FromQuery] int pageNumber = 1,
                                   [FromQuery] string product = "",
                                   [FromQuery] int? category = null)
        {

            var data = _service.GetProducts(pageSize, pageNumber, product, category);
            if (data.TotalPages < pageNumber)
            {
                data = _service.GetProducts(pageSize, 1, product, category);
            }

            var viewModel = new ProductGridViewModel { Dto = data, Product = product, Category = category };
            var insertViewModel = new ProductInsertViewModel(_service.GetCategoryDropdown());
            var updateViewModel = new ProductUpdateViewModel(_service.GetCategoryDropdown());

            var combinedViewModel = new AllProductViewModels
            {
                ProductGridViewModel = viewModel,
                ProductInsertViewModel = insertViewModel,
                ProductUpdateViewModel = updateViewModel
            };

            return View(combinedViewModel);
        }


     
    }
}
