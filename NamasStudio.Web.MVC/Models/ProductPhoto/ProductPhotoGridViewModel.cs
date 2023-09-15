using NamasStudio.Dto;
using NamasStudio.Dto.ProductPhoto;

namespace NamasStudio.Web.MVC.Models.ProductPhoto
{
    public class ProductPhotoGridViewModel
    {
        public Grid<ProductPhotoGridDto> Dto { get; set; }
        public int ProductId { get; set; }  
    }
}
