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

    internal class ProductPhotoRepo : IProductPhotoRepo
    {
        private readonly NamasStudioContext _dbContext;
        public ProductPhotoRepo(NamasStudioContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public Grid<PhotoProduct> FindAllProductPhotoByProductId(int productId)
        {
            IQueryable<PhotoProduct> query = _dbContext.PhotoProducts
                                            .Where(x => x.ProductId == productId);
            return new Grid<PhotoProduct>
            {
                Count = query.Count(),
                Data = query.ToList(),
                PageNumber = 0,
                PageSize = 0,
            };
        }

        public PhotoProduct CreateProductPhoto(PhotoProduct photoProduct)
        {
            _dbContext.Add(photoProduct);
            _dbContext.SaveChanges();
            return photoProduct;
        }

        public int CountPhotoByProductId() {
            var query = _dbContext.PhotoProducts.OrderBy(x => x.PhotoId).LastOrDefault();
            if (query != null) {
                return query.PhotoId;
            }
            return 0;
        }

        public PhotoProduct DeleteProductPhoto(int photoProductId)
        {
            var photo = _dbContext.PhotoProducts.Where(x => x.PhotoId == photoProductId).FirstOrDefault();
            if (photo != null) {
                _dbContext.Remove(photo);
                _dbContext.SaveChanges();
                return photo;
            }
            else
            {
                throw new Exception("Photo doesnt exist");
            }

        }

        public string GetPathPhoto(int photoProductId)
        {
            var photo = _dbContext.PhotoProducts.Where(x => x.PhotoId == photoProductId).FirstOrDefault();
            return photo.PathName;
        }
    }
}
