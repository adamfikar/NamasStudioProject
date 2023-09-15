using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NamasStudio.Dto.ProductStock;
using NamasStudio.Services.Services.Interfaces;

namespace NamasStudio.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStockApiController : ControllerBase
    {
        private readonly IProductStockService _service;

        public ProductStockApiController(IProductStockService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<ActionResult> InsertProduct([FromBody] ProductStockInsertDto dto)
        {
            if (!ModelState.IsValid)
            {
                // If the ModelState is not valid, return the validation errors to the client.
                var errors = ModelState.Values
                           .SelectMany(v => v.Errors)
                           .Select(e => e.ErrorMessage);

                return BadRequest(errors);
            }

            var response = _service.InsertProductStock(dto);
            return Ok(response);
        }


        [HttpGet]
        [Route("GetSizeDropdown")]
        public async Task<ActionResult> GetSizeDropdown(int categoryId)
        {
            var dropdown = await _service.GetSizeDropdown(categoryId);
            return Ok(dropdown);
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteProduct(int productId, int sizeId)
        {
            try
            {
                _service.DeleteProductStock(productId, sizeId);
                return Ok();
            }
            catch
            {
                // Handle any exceptions here
                return BadRequest("An error occurred while deleting the size.");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update([FromBody] ProductStockUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                // If the ModelState is not valid, return the validation errors to the client.
                var errors = ModelState.Values
                           .SelectMany(v => v.Errors)
                           .Select(e => e.ErrorMessage);

                return BadRequest(errors);
            }

            _service.UpdateProductStock(dto);
            return Ok();
        }

        [HttpGet]
        [Route("GetProductStockById")]
        public IActionResult GetProductStock(int productId,int sizeId)
        {
            var product = _service.GetProductStockById(productId,sizeId);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

    }
}
