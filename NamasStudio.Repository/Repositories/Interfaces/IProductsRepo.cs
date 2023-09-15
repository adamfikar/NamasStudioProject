using NamasStudio.DataAccess.Models;
using NamasStudio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Repository.Repositories.Interfaces
{
    public interface IProductsRepo
    {
       public Product CreateProduct(Product product);
        Product DeleteProduct(int productId);
        public Grid<Product> FindAllProducts(int pageSize, int pageNumber, string product, int? categoryId);
        Product FindProductById(int productId);
        Product UpdateProduct(Product product);
    }
}
