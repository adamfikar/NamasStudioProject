using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NamasStudio.Dto.Product;
using NamasStudio.Services.Services.Interfaces;

namespace NamasStudio.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductsService _service;

        public ProductsApiController(IProductsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetAllProduct")]
        public IActionResult Index([FromQuery] int pageSize = 10,
                                   [FromQuery] int pageNumber = 1,
                                   [FromQuery] string? product = null,
                                   [FromQuery] int? category = null)
        {
            var data = _service.GetProducts(pageSize, pageNumber, product, category);
            if (data.TotalPages < pageNumber)
            {
                data = _service.GetProducts(pageSize, 1, product ?? "", category );
            }

            return Ok(data);
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult InsertProduct([FromBody] ProductInsertDto dto)
        {
            if (!ModelState.IsValid)
            {
                // If the ModelState is not valid, return the validation errors to the client.
                var errors = ModelState.Values
                           .SelectMany(v => v.Errors)
                           .Select(e => e.ErrorMessage);

                return BadRequest(errors);
            }

            var response = _service.InsertProduct(dto);
            return Ok(response);
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteProduct(int productId)
        {
            try
            {
                _service.DeleteProduct(productId);
                return Ok();
            }
            catch
            {
                // Handle any exceptions here
                return BadRequest("An error occurred while deleting the product.");
            }
        }

        [HttpGet]
        [Route("GetProductById")]
        public IActionResult GetProduct(int productId)
        {
            var product = _service.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] ProductUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                // If the ModelState is not valid, return the validation errors to the client.
                var errors = ModelState.Values
                           .SelectMany(v => v.Errors)
                           .Select(e => e.ErrorMessage);

                return BadRequest(errors);
            }

            _service.UpdateProduct(dto);
            return Ok();
        }
    }
}
