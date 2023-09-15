﻿using Microsoft.AspNetCore.Mvc.Rendering;
using NamasStudio.Dto;
using NamasStudio.Dto.Product;

namespace NamasStudio.Web.MVC.Models.Products
{
    public class ProductInsertViewModel
    {
        public ProductInsertDto Dto { get; set; }
        public List<SelectListItem> CategoryClassDropdown { get; set; }

        public ProductInsertViewModel(List<DropdownDto> categoryDropdown)
        {
      
            CategoryClassDropdown = categoryDropdown.ConvertAll(item => new SelectListItem
            {
                Value = item.Value.ToString(),
                Text = item.Text
            });
        }

    }
}
