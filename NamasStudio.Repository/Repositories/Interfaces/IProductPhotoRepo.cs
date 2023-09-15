using NamasStudio.DataAccess.Models;
using NamasStudio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Repository.Repositories.Interfaces
{
    public interface IProductPhotoRepo
    {
        int CountPhotoByProductId();
        PhotoProduct CreateProductPhoto(PhotoProduct photoProduct);
        PhotoProduct DeleteProductPhoto(int photoProductId);
        Grid<PhotoProduct> FindAllProductPhotoByProductId(int productId);
        string GetPathPhoto(int photoProductId);
    }
}
