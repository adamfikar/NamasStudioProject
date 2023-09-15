using AutoMapper;
using NamasStudio.DataAccess.Models;
using NamasStudio.Dto.Category;
using NamasStudio.Dto.Product;
using NamasStudio.Dto.ProductPhoto;
using NamasStudio.Dto.ProductSize;
using NamasStudio.Dto.ProductStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Services.Configs
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CategoryAutoMapper();
            ProductsAutoMapper();
            ProductStockAutoMapper();
            ProductSizeAutoMapper();
            ProductPhotoAutoMapper();
        }

        private void CategoryAutoMapper()
        {
            CreateMap<CategoryProduct, CategoryProductGridDto>();

            CreateMap<CategoryProduct, CategoryProductInsertDto>();
            CreateMap<CategoryProductInsertDto, CategoryProduct>();

            CreateMap<CategoryProductDetailsDto, CategoryProduct>();
            CreateMap<CategoryProduct, CategoryProductDetailsDto>();

            CreateMap<CategoryProductUpdateDto, CategoryProduct>();
            CreateMap<CategoryProduct, CategoryProductUpdateDto>();
        }

        private void ProductsAutoMapper()
        {
            CreateMap<Product, ProductGridDto>().ForMember(d => d.CategoryName, c => c.MapFrom(f => f.Category.CategoryName))
                                                .ForMember(d => d.UnitPrice, c => c.MapFrom(f => f.UnitPrice.ToString("C")));

            CreateMap<Product, ProductInsertDto>();
            CreateMap<ProductInsertDto, Product>();

            CreateMap<Product, ProductDetailsDto>();
            CreateMap<ProductDetailsDto, Product>();

            CreateMap<Product, ProductUpdateDto>();
            CreateMap<ProductUpdateDto, Product>();
        }

        private void ProductStockAutoMapper()
        {
            CreateMap<StockProduct, ProductStockGridDto>().ForMember(x => x.SizeName, c => c.MapFrom(d => d.Size.SizeName));

            CreateMap<StockProduct, ProductStockInsertDto>();
            CreateMap<ProductStockInsertDto, StockProduct>();

            CreateMap<StockProduct, ProductStockUpdateDto>();
            CreateMap<ProductStockUpdateDto, StockProduct>();

            CreateMap<StockProduct, ProductStockDetailsDto>().ForMember(x => x.SizeName, c => c.MapFrom(d => d.Size.SizeName));
            CreateMap<ProductStockDetailsDto, StockProduct>();
        }

        private void ProductSizeAutoMapper()
        {
            CreateMap<SizeCategory, ProductSizeGridDto>().ForMember(x => x.SizeName, c => c.MapFrom(d => d.ProductSize.SizeName))
                                                         .ForMember(x => x.CategoryName, c => c.MapFrom(d => d.CategoryProduct.CategoryName))
                                                         .ForMember(x => x.Waist, c => c.MapFrom(d => d.ProductSize.Waist))
                                                         .ForMember(x => x.Hips, c => c.MapFrom(d => d.ProductSize.Hips))
                                                         .ForMember(x => x.LengthLower, c => c.MapFrom(d => d.ProductSize.LengthLower))
                                                         .ForMember(x => x.Bust, c => c.MapFrom(d => d.ProductSize.Bust))
                                                         .ForMember(x => x.LengthUpper, c => c.MapFrom(d => d.ProductSize.LengthUpper))
                                                         .ForMember(x => x.ArmHole, c => c.MapFrom(d => d.ProductSize.ArmHole))
                                                         .ForMember(x => x.BottomSleeve, c => c.MapFrom(d => d.ProductSize.BottomSleeve))
                                                         .ForMember(x => x.SleeveLength, c => c.MapFrom(d => d.ProductSize.SleeveLength))
                                                         .ForMember(x => x.Description, c => c.MapFrom(d => d.ProductSize.Description));

            CreateMap<ProductSize, ProductSizeInsertDto>();
            CreateMap<ProductSizeInsertDto, ProductSize>();

            CreateMap<ProductSize, ProductSizeUpdateDto>();
            CreateMap<ProductSizeUpdateDto, ProductSize>();

            CreateMap<ProductSize, ProductSizeDetailsDto>();
            CreateMap<ProductSizeDetailsDto, ProductSize>();
        }

        private void ProductPhotoAutoMapper() {
            /*  CreateMap<PhotoProduct, ProductPhotoInsertDto>();
                CreateMap<ProductPhotoInsertDto, PhotoProduct>();
            */

            CreateMap<PhotoProduct, ProductPhotoGridDto>();
        }
    }
}
