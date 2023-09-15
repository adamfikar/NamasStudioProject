using NamasStudio.DataAccess.Models;
using NamasStudio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Repository.Repositories.Interfaces
{
    public interface IProductStockRepo
    {
        StockProduct CreateProductStock(StockProduct productStock);
        StockProduct DeleteProductStock(int productId, int sizeId);
        Grid<StockProduct> FindAllStockByProduct(int pageSize, int pageNumber, int productId);
        StockProduct FindProductStockById(int productId, int sizeId);
        StockProduct FindProductStockBySizeId(int sizeId);
        StockProduct UpdateProductStock(StockProduct product);
    }
}
