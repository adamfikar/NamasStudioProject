using Microsoft.AspNetCore.Http;
using NamasStudio.Dto;
using NamasStudio.Dto.ProductPhoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Services.Services.Interfaces
{
    public interface IProductPhotoService
    {
        void DeletePhoto(int photoId);
        Grid<ProductPhotoGridDto> GetAllProductPhotoByProductId(int productId);
        Task<ProductPhotoInsertDto> InsertProductPhoto(IFormFile img, ProductPhotoInsertDto dto);
    }
}
