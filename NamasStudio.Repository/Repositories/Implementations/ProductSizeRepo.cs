using Microsoft.EntityFrameworkCore;
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
    internal class ProductSizeRepo : IProductSizeRepo
    {
        private readonly NamasStudioContext _dbContext;

        public ProductSizeRepo(NamasStudioContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Grid<SizeCategory> FindAllProductSize(int pageSize, int pageNumber, string sizeName)
        {
            IQueryable<SizeCategory> query = _dbContext.SizeCategory
                                            .Where(x => x.ProductSize.SizeName.Contains(sizeName ?? string.Empty))
                                            .Include(s => s.ProductSize)
                                            .Include(s => s.CategoryProduct)
                                            .OrderBy(x => x.CategoryProduct.CategoryName);
            return new Grid<SizeCategory>
            {
                Count = query.Count(),
                Data = query.Skip(pageSize * (pageNumber - 1))
                       .Take(pageSize)
                        .ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }

        public List<DropdownDto> FindSizeDropdown(int categoryId)
        {

            //var query = _dbContext.ProductSizes.Select(t => new DropdownDto(t.SizeId, t.SizeName)).ToList();
            var query = _dbContext.SizeCategory.Where(x => x.CategoryId == categoryId)
                        .Select(t => new DropdownDto(t.SizeId, t.ProductSize.SizeName)).ToList();
            return query;
        }

        public ProductSize CreateProductSize(ProductSize productSize)
        {
            _dbContext.Add(productSize);
            _dbContext.SaveChanges();
            return productSize;
        }

        public SizeCategory CreateSizeCategory(SizeCategory sizeCategory)
        {
            _dbContext.Add(sizeCategory);
            _dbContext.SaveChanges();
            return sizeCategory;
        }

        public ProductSize DeleteProductSize(int sizeId)
        {
            var productSize = _dbContext.ProductSizes.Where(d => d.SizeId == sizeId).FirstOrDefault();
            _dbContext.Remove(productSize);
            _dbContext.SaveChanges();
            return productSize;
        }

        public SizeCategory DeleteSizeCategory(int sizeId, int categoryId)
        {
            var sizeCategory = _dbContext.SizeCategory.Where(d => d.SizeId == sizeId && d.CategoryId == categoryId).FirstOrDefault();
            _dbContext.Remove(sizeCategory);
            _dbContext.SaveChanges();
            return sizeCategory;
        }

        public ProductSize UpdateProductSize(ProductSize productSize)
        {
            //_dbContext.ChangeTracker.Clear();
            _dbContext.Update(productSize);
            _dbContext.SaveChanges();
            return productSize;
        }

        public ProductSize FindProductSizeById(int sizeId)
        {
            var entity = _dbContext.ProductSizes.Include(x => x.Sizes).FirstOrDefault(c => c.SizeId == sizeId)
            ?? throw new ArgumentException("Size not found!!");
            return entity!;
        }

    }
}
