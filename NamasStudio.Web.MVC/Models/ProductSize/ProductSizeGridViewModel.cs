using NamasStudio.Dto;
using NamasStudio.Dto.ProductSize;

namespace NamasStudio.Web.MVC.Models.ProductSize
{
    public class ProductSizeGridViewModel
    {
        public Grid<ProductSizeGridDto> Dto { get; set; }

        public string Size { get; set; }    
    }
}
