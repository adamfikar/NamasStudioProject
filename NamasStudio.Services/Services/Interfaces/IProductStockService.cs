using NamasStudio.Dto;
using NamasStudio.Dto.Product;
using NamasStudio.Dto.ProductStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Services.Services.Interfaces
{
    public interface IProductStockService
    {
        void DeleteProductStock(int productId, int sizeId);
        Grid<ProductStockGridDto> GetAllStockProductById(int pageSize, int pageNumber, int productId);
        ProductDetailsDto GetProductById(int productId);
        ProductStockDetailsDto GetProductStockById(int productId, int sizeId);
        Task<List<DropdownDto>> GetSizeDropdown(int categoryId);
        ProductStockInsertDto InsertProductStock(ProductStockInsertDto dto);
        ProductStockUpdateDto UpdateProductStock(ProductStockUpdateDto dto);
    }
}
