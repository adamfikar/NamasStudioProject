using NamasStudio.DataAccess.Models;
using NamasStudio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Repository.Repositories.Interfaces
{
    public interface IProductSizeRepo
    {
        ProductSize CreateProductSize(ProductSize productSize);
        SizeCategory CreateSizeCategory(SizeCategory sizeCategory);
        ProductSize DeleteProductSize(int sizeId);
        SizeCategory DeleteSizeCategory(int sizeId, int categoryId);
        Grid<SizeCategory> FindAllProductSize(int pageSize, int pageNumber, string sizeName);
        ProductSize FindProductSizeById(int sizeId);
        List<DropdownDto> FindSizeDropdown(int categoryId);
        ProductSize UpdateProductSize(ProductSize productSize);
    }
}
