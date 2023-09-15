using NamasStudio.DataAccess.Models;
using NamasStudio.Dto;
using NamasStudio.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NamasStudio.Repository.Repositories.Implementations
{
    internal class CategoryProductRepo : ICategoryProductRepo
    {
        private readonly NamasStudioContext _dbContext;

        public CategoryProductRepo(NamasStudioContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Grid<CategoryProduct> FindAllCategoryProducts(int pageSize, int pageNumber, string? category)
        {
            IQueryable<CategoryProduct> query = from s in _dbContext.CategoryProducts
                                                where s.CategoryName.Contains(category ?? string.Empty)
                                                select s;
            return new Grid<CategoryProduct>
            {
                Count = query.Count(),
                Data = query.Skip(pageSize * (pageNumber - 1))
                       .Take(pageSize)
                        .ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }

        public CategoryProduct CreateCategoryProduct(CategoryProduct categoryProduct)
        {
            _dbContext.Add(categoryProduct);
            _dbContext.SaveChanges();
            return categoryProduct;
        }

        public CategoryProduct DeleteCategoryProduct(int categoryProductId)
        {
            var checkCategory = _dbContext.Products.FirstOrDefault(p => p.CategoryId == categoryProductId);
            if (checkCategory == null)
            {
                var category = _dbContext.CategoryProducts.Where(d => d.CategoryId == categoryProductId).FirstOrDefault();
                _dbContext.Remove(category);
                _dbContext.SaveChanges();
                return category;
            }
            else {
                throw new Exception("Category associated with product");
            }
        }

        public CategoryProduct FindCategoryProductById(long categoryProductId)
        {
            var entity = _dbContext.CategoryProducts.FirstOrDefault(c => c.CategoryId == categoryProductId)
            ?? throw new ArgumentException("Category Product not found!!");
            return entity!;
        }

        public CategoryProduct UpdateCategoryProduct(CategoryProduct categoryProduct)
        {
            //_dbContext.ChangeTracker.Clear();
            _dbContext.Update(categoryProduct);
            _dbContext.SaveChanges();
            return categoryProduct;
        }

        public List<DropdownDto> FindCategoryDropdown()
        {
            var query = _dbContext.CategoryProducts.Select(t => new DropdownDto(t.CategoryId, t.CategoryName)).ToList();
            return query;
        }
    }
}
