using NamasStudio.DataAccess.Models;
using NamasStudio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NamasStudio.Repository.Repositories.Interfaces
{
    public interface ICategoryProductRepo
    {
        public CategoryProduct CreateCategoryProduct(CategoryProduct categoryProduct);
        CategoryProduct DeleteCategoryProduct(int categoryProductId);
        public Grid<CategoryProduct> FindAllCategoryProducts(int pageSize, int pageNumber, string? category);
        List<DropdownDto> FindCategoryDropdown();
        CategoryProduct FindCategoryProductById(long categoryProductId);
        CategoryProduct UpdateCategoryProduct(CategoryProduct categoryProduct);
    }
}
