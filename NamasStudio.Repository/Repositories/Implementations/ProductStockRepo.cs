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
    internal class ProductStockRepo : IProductStockRepo
    {
        private readonly NamasStudioContext _dbContext;

        public ProductStockRepo(NamasStudioContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Grid<StockProduct> FindAllStockByProduct(int pageSize, int pageNumber, int productId)
        {
            IQueryable<StockProduct> query = _dbContext.StockProducts.Where(x => x.ProductId == productId)
                                            .Include(x => x.Size)
                                            .Include(x => x.Product);

            return new Grid<StockProduct>
            {
                Count = query.Count(),
                Data = query.Skip(pageSize * (pageNumber - 1))
                       .Take(pageSize)
                       .ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }

        public StockProduct CreateProductStock(StockProduct productStock)
        {
            _dbContext.Add(productStock);
            _dbContext.SaveChanges();
            return productStock;
        }

        public StockProduct DeleteProductStock(int productId, int sizeId)
        {
            var size = _dbContext.StockProducts.Where(d => d.ProductId == productId && d.SizeId == sizeId).FirstOrDefault();
            _dbContext.Remove(size);
            _dbContext.SaveChanges();
            return size;
        }

        public StockProduct FindProductStockById(int productId, int sizeId)
        {
            var entity = _dbContext.StockProducts.Include(x => x.Size).FirstOrDefault(c => c.ProductId == productId && c.SizeId == sizeId)
            ?? throw new ArgumentException("Size not found!!");
            return entity!;
        }

        public StockProduct UpdateProductStock(StockProduct product)
        {
            //_dbContext.ChangeTracker.Clear();
            _dbContext.Update(product);
            _dbContext.SaveChanges();
            return product;
        }

        public StockProduct FindProductStockBySizeId(int sizeId)
        {
            var entity = _dbContext.StockProducts.FirstOrDefault(c => c.SizeId == sizeId);
            return entity!;
        }
    }
}
