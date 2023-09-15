using Microsoft.AspNetCore.Mvc.Rendering;
using NamasStudio.Dto;
using NamasStudio.Dto.Product;

namespace NamasStudio.Web.MVC.Models.Products
{
    public class ProductUpdateViewModel
    {
        public ProductUpdateDto Dto { get; set; }
        public List<SelectListItem> CategoryClassDropdown { get; set; }

        public ProductUpdateViewModel(List<DropdownDto> categoryDropdown)
        {

            CategoryClassDropdown = categoryDropdown.ConvertAll(item => new SelectListItem
            {
                Value = item.Value.ToString(),
                Text = item.Text
            });
        }
    }
}
