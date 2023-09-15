using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NamasStudio.Dto.ProductPhoto;
using NamasStudio.Services.Services.Interfaces;
using NamasStudio.Web.MVC.Models.ProductPhoto;

namespace NamasStudio.Web.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductPhotoController : Controller
    {

        private readonly IProductPhotoService _service;

        public ProductPhotoController(IProductPhotoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] int productId)
        {

            var data = _service.GetAllProductPhotoByProductId(productId);

            var viewModel = new ProductPhotoGridViewModel { Dto = data, ProductId = productId };

            return View(viewModel);
        }

        [HttpPost("UploadPhoto")]
        public async Task<IActionResult> Add(IFormFile img, int productId)
        {

            // You can set it to the desired productId here

            if (img != null)
            {
                var dto = new ProductPhotoInsertDto
                {
                    ProductId = productId,
                    PathName = img.FileName

                };

                await _service.InsertProductPhoto(img, dto);
            }

            // This should generate a URL like /PhotoIndex?productId=1
            return RedirectToAction("Index", new { productId = productId });
        }

    }
}
