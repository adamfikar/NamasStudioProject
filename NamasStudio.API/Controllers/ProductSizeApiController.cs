using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NamasStudio.Dto.ProductSize;
using NamasStudio.Services.Services.Interfaces;

namespace NamasStudio.API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSizeApiController : ControllerBase
    {
        private readonly IProductSizeService _service;

        public ProductSizeApiController(IProductSizeService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetAllProductsSize")]
        public async Task<ActionResult> Index([FromQuery] int pageSize = 10,
                                              [FromQuery] int pageNumber = 1,
                                              [FromQuery] string sizeName = "") // Make the category parameter nullable
        {
            // Handle an empty category value and set it to null
            var data = _service.GetProductSize(pageSize, pageNumber,sizeName);

            if (data.TotalPages < pageNumber)
            {
                data = _service.GetProductSize(pageSize, 1, sizeName);
            }

            return Ok(data);
        }


        [HttpPost]
        [Route("Insert")]
        public IActionResult InsertProductSize([FromBody] ProductSizeInsertDto dto)
        {
            if (!ModelState.IsValid)
            {
                // If the ModelState is not valid, return the validation errors to the client.
                var errors = ModelState.Values
                           .SelectMany(v => v.Errors)
                           .Select(e => e.ErrorMessage);

                return BadRequest(errors);
            }

            var response = _service.InsertProductSize(dto);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetCategoryDropdown")]
        public async Task<ActionResult> GetCategoryDropdown()
        {
            var dropdown = await _service.GetCategoryDropdown();
            return Ok(dropdown);
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteProductSize(int sizeId,int categoryId)
        {
            try
            {
                _service.DeleteProductSize(sizeId, categoryId);
                return Ok();
            }
            catch(Exception e)
            {
                // Handle any exceptions here
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update([FromBody] ProductSizeUpdateDto dto)
        {
            await _service.UpdateProductSize(dto);
            return Ok();
        }

        [HttpGet]
        [Route("GetProductSize")]
        public async Task<ActionResult> GetId(int sizeId)
        {
            var productSize = await _service.GetProductSizeById(sizeId);
            if (productSize == null)
            {
                return NotFound();
            }
            return Ok(productSize);
        }
    }
}
