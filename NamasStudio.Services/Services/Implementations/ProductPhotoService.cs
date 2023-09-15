using AutoMapper;
using Microsoft.AspNetCore.Http;
using NamasStudio.DataAccess.Models;
using NamasStudio.Dto;
using NamasStudio.Dto.ProductPhoto;
using NamasStudio.Repository.Repositories.Interfaces;
using NamasStudio.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Services.Services.Implementations
{
   
    internal class ProductPhotoService : IProductPhotoService
    {
        private readonly IProductPhotoRepo _productPhotoRepo;
        private readonly IProductsRepo _productRepo;
        private readonly IMapper _mapper;


        public ProductPhotoService(IProductPhotoRepo productPhotoRepo, IProductsRepo productRepo, IMapper mapper)
        {
            _productPhotoRepo = productPhotoRepo;

            _productRepo = productRepo;
            _mapper = mapper;

     
        }

        public Grid<ProductPhotoGridDto> GetAllProductPhotoByProductId(int productId)
        {
            var list = _productPhotoRepo.FindAllProductPhotoByProductId(productId);

            return new Grid<ProductPhotoGridDto>
            {
                Count = list.Count,
                Data = list.Data.ConvertAll(d => _mapper.Map<ProductPhotoGridDto>(d)),
                PageNumber = list.PageNumber,
                PageSize = list.PageSize
            };
        }

        public async Task<ProductPhotoInsertDto> InsertProductPhoto(IFormFile img,ProductPhotoInsertDto dto)
        {

            if (img != null && img.Length > 0)
            {
                var fileName = UploadFoto(img, dto.ProductId);

                var photo = new PhotoProduct
                {
                    ProductId = dto.ProductId,
                    PathName = fileName
                };

                _productPhotoRepo.CreateProductPhoto(photo);
                return dto;
            }
            return null;
        }

        private string UploadFoto(IFormFile photo, int id)
        {
            var product = _productRepo.FindProductById(id);

            var productName = $"{product.ProductName}-{product.Color}-{GetPhotoIdByProductId()}";
            var fileName = productName + Path.GetExtension(photo.FileName);

            string sPath = @"C:\NamasStudio.Web.MVC\NamasStudio.Web.MVC\wwwroot\";
            // Save the photo to a file on the server using the _wwwrootPath
            var filePath = Path.Combine(sPath, "img", "productImage", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                photo.CopyTo(stream);
            }

            return fileName;
        }

        private int GetPhotoIdByProductId() {
            var photoId = _productPhotoRepo.CountPhotoByProductId();
            if (photoId != null || photoId > 0) {
                return photoId + 1;
            }
            return 1;
        }

        public void DeletePhoto(int photoId) {
            var pathName = _productPhotoRepo.GetPathPhoto(photoId);

            string sPath = @"C:\NamasStudio.Web.MVC\NamasStudio.Web.MVC\wwwroot\";
            var filePath = Path.Combine(sPath, "img", "productImage", pathName);
            if (File.Exists(filePath))
            {
                // Delete the file
                File.Delete(filePath);
                _productPhotoRepo.DeleteProductPhoto(photoId);
            }

        }

    }
}
