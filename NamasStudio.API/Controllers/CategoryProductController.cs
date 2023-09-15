using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NamasStudio.API.Filters;
using NamasStudio.Dto.Category;
using NamasStudio.Services.Services.Interfaces;

namespace NamasStudio.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryProductController : ControllerBase
    {
        private readonly ICategoryProductService _service;

        public CategoryProductController(ICategoryProductService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetAllCategory")]
        public async Task<ActionResult> Index([FromQuery] int pageSize = 10,
                                              [FromQuery] int pageNumber = 1,
                                              [FromQuery] string? category = null) // Make the category parameter nullable
        {

            // Handle an empty category value and set it to null
            if (string.IsNullOrWhiteSpace(category))
            {
                category = string.Empty;
            }

            var data = _service.GetCategory(pageSize, pageNumber, category);

            if (data.TotalPages < pageNumber)
            {
                data = _service.GetCategory(pageSize, 1, category ?? "");
            }

            return Ok(data);
        }



        [HttpPost]
        [Route("Insert")]
        public async Task<ActionResult> InsertCategory([FromBody] CategoryProductInsertDto dto)
        {
            if (!ModelState.IsValid)
            {
                // If the ModelState is not valid, return the validation errors to the client.
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(errors);
            }

            var response = await _service.InsertCategory(dto);
            return Ok(response);
        }


        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            try
            {
                _service.DeleteCategory(categoryId);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetCategory")]
        public async Task<ActionResult> GetId(long categoryId)
        {
            var category = await _service.GetCategoryById(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update([FromBody] CategoryProductUpdateDto dto)
        {
            await _service.UpdateCategory(dto);
            return Ok();
        }
    }
}
