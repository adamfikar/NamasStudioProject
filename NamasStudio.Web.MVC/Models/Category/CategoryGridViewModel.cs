using NamasStudio.Dto.Category;
using NamasStudio.Dto;

namespace NamasStudio.Web.MVC.Models.Category
{
    public class CategoryGridViewModel
    {
        public Grid<CategoryProductGridDto> Dto { get; set; }

        public string Category { get; set; }
    }
}
