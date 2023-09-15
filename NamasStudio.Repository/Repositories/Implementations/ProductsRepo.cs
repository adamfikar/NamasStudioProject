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
    internal class ProductsRepo : IProductsRepo
    {
        private readonly NamasStudioContext _dbContext;

        public ProductsRepo(NamasStudioContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Grid<Product> FindAllProducts(int pageSize, int pageNumber, string product, int? categoryId)
        {
            IQueryable<Product> query = _dbContext.Products.Where(x => x.ProductName.Contains(product ?? string.Empty)
                                        && (categoryId == null || x.CategoryId == categoryId)).OrderBy(x => x.ProductName)
                                        .Include(x => x.Category);  

            return new Grid<Product>
            {
                Count = query.Count(),
                Data = query.Skip(pageSize * (pageNumber - 1))
                       .Take(pageSize)
                       .ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }

        public Product CreateProduct(Product product)
        {
            _dbContext.Add(product);
            _dbContext.SaveChanges();
            return product;
        }

        public Product DeleteProduct(int productId)
        {
            var product = _dbContext.Products.Where(d => d.ProductId == productId).FirstOrDefault();
            _dbContext.Remove(product);
            _dbContext.SaveChanges();
            return product;
        }

        public Product FindProductById(int productId)
        {
            var entity = _dbContext.Products.FirstOrDefault(c => c.ProductId == productId)
            ?? throw new ArgumentException("Product not found!!");
            return entity!;
        }

        public Product UpdateProduct(Product product)
        {
            //_dbContext.ChangeTracker.Clear();
            _dbContext.Update(product);
            _dbContext.SaveChanges();
            return product;
        }
    }
}
