using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NamasStudio.Dto.ProductPhoto;
using NamasStudio.Services.Services.Interfaces;

namespace NamasStudio.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPhotoApiController : ControllerBase
    {
        private readonly IProductPhotoService _service;

        public ProductPhotoApiController(IProductPhotoService service)
        {
            _service = service;
        }

        [HttpPost("UploadPhoto")]
        public async Task<ActionResult> Add(IFormFile img, [FromQuery] int productId)
        {
            if (img != null)
            {
                const long maxFileSize = 1 * 1024 * 1024; // 1MB
                if (img.Length <= maxFileSize)
                {
                    string[] listType = { ".jpg", ".png", ".jpeg" };
                    var type = Path.GetExtension(img.FileName);
                    if (!listType.Contains(type))
                    {
                        return BadRequest("Invalid Type");
                    }

                    var dto = new ProductPhotoInsertDto
                    {
                        ProductId = productId,
                        PathName = img.FileName
                    };

                    await _service.InsertProductPhoto(img, dto);
                }
                else {
                    return BadRequest("File shouldnt exceed than 1MB");
                
                }
             
            }

            // You can return a status code or a response here if needed
            return Ok();
        }


        [HttpDelete("DeletePhoto")]
        public async Task<ActionResult> Delete([FromQuery] int photoId)
        {
            _service.DeletePhoto(photoId);
            return Ok();
        }
    }
}
