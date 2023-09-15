using NamasStudio.Dto;
using NamasStudio.Dto.ProductSize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Services.Services.Interfaces
{
    public interface IProductSizeService
    {
        void DeleteProductSize(int productId, int categoryId);
        Task<List<DropdownDto>> GetCategoryDropdown();
        Grid<ProductSizeGridDto> GetProductSize(int pageSize, int pageNumber, string sizeName);
        Task<ProductSizeDetailsDto> GetProductSizeById(int sizeId);
        Task<ProductSizeInsertDto> InsertProductSize(ProductSizeInsertDto dto);
        Task<ProductSizeUpdateDto> UpdateProductSize(ProductSizeUpdateDto dto);
    }
}
