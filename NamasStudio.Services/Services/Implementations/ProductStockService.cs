using AutoMapper;
using NamasStudio.DataAccess.Models;
using NamasStudio.Dto;
using NamasStudio.Dto.Product;
using NamasStudio.Dto.ProductStock;
using NamasStudio.Repository.Repositories.Interfaces;
using NamasStudio.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Services.Services.Implementations
{
    internal class ProductStockService : IProductStockService
    {
        private readonly IProductStockRepo _productStockRepo;
        private readonly IProductsRepo _productRepo;
        private readonly IProductSizeRepo _productSizeRepo;
        private readonly IMapper _mapper;

        public ProductStockService(IProductStockRepo productStockRepo, IProductsRepo productRepo, IProductSizeRepo productSizeRepo, IMapper mapper)
        {
            _productStockRepo = productStockRepo;
            _productRepo = productRepo;
            _productSizeRepo = productSizeRepo;
            _mapper = mapper;
        }

        public Grid<ProductStockGridDto> GetAllStockProductById(int pageSize, int pageNumber, int productId)
        {
            var list = _productStockRepo.FindAllStockByProduct(pageSize, pageNumber, productId);
            return new Grid<ProductStockGridDto>
            {
                Count = list.Count,
                Data = list.Data.ConvertAll(d => _mapper.Map<ProductStockGridDto>(d)),
                PageNumber = list.PageNumber,
                PageSize = list.PageSize
            };
        }

        public ProductDetailsDto GetProductById(int productId)
        {
            var result = _productRepo.FindProductById(productId);
            return _mapper.Map<ProductDetailsDto>(result);
        }

        public ProductStockInsertDto InsertProductStock(ProductStockInsertDto dto)
        {
            var product = _mapper.Map<StockProduct>(dto);
            _productStockRepo.CreateProductStock(product);
            return _mapper.Map<ProductStockInsertDto>(product);
        }

        public async Task<List<DropdownDto>> GetSizeDropdown(int categoryId)
        {
            return _productSizeRepo.FindSizeDropdown(categoryId);
        }

        public void DeleteProductStock(int productId, int sizeId)
        {
            _productStockRepo.DeleteProductStock(productId, sizeId);
        }
        public ProductStockDetailsDto GetProductStockById(int productId, int sizeId)
        {
            var result = _productStockRepo.FindProductStockById(productId,sizeId);
            return _mapper.Map<ProductStockDetailsDto>(result);
        }

        public ProductStockUpdateDto UpdateProductStock(ProductStockUpdateDto dto)
        {
            var productStock = _productStockRepo.FindProductStockById(dto.ProductId,dto.SizeId);
            _mapper.Map(dto, productStock);
            productStock = _productStockRepo.UpdateProductStock(productStock);
            return _mapper.Map<ProductStockUpdateDto>(productStock);
        }

    }
}
