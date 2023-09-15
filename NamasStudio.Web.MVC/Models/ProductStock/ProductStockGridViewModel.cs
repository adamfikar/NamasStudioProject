using NamasStudio.Dto;
using NamasStudio.Dto.Product;
using NamasStudio.Dto.ProductStock;

namespace NamasStudio.Web.MVC.Models.ProductStock
{
    public class ProductStockGridViewModel
    {
        public Grid<ProductStockGridDto> Dto { get; set; }

        public ProductDetailsDto Product { get; set; }
    }
}
