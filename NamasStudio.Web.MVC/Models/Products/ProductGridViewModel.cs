using NamasStudio.Dto;
using NamasStudio.Dto.Product;

namespace NamasStudio.Web.MVC.Models.Products
{
    public class ProductGridViewModel
    {
        public Grid<ProductGridDto> Dto { get; set; }

        public string Product { get; set; }

        public int? Category { get; set; }
    }
}
