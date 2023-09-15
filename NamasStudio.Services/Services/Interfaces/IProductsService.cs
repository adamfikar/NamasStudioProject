using NamasStudio.Dto;
using NamasStudio.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Services.Services.Interfaces
{
    public interface IProductsService
    {
        void DeleteProduct(int productId);
        ProductUpdateDto GetProductById(int productId);
        List<DropdownDto> GetCategoryDropdown();
        Grid<ProductGridDto> GetProducts(int pageSize, int pageNumber, string product, int? categoryId);
        ProductInsertDto InsertProduct(ProductInsertDto dto);
        ProductUpdateDto UpdateProduct(ProductUpdateDto dto);
    }
}
