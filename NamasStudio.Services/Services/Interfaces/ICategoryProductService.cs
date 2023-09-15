using NamasStudio.Dto;
using NamasStudio.Dto.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NamasStudio.Services.Services.Interfaces
{
    public interface ICategoryProductService
    {
        void DeleteCategory(int categoryId);
       Grid<CategoryProductGridDto> GetCategory(int pageSize, int pageNumber, string? category);
        Task<CategoryProductDetailsDto> GetCategoryById(long id);
        Task<CategoryProductInsertDto> InsertCategory(CategoryProductInsertDto dto);
        Task<CategoryProductUpdateDto> UpdateCategory(CategoryProductUpdateDto dto);
    }
}
