using AutoMapper;
using NamasStudio.DataAccess.Models;
using NamasStudio.Dto;
using NamasStudio.Dto.ProductSize;
using NamasStudio.Repository.Repositories.Interfaces;
using NamasStudio.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Services.Services.Implementations
{
    internal class ProductSizeService : IProductSizeService
    {
        private readonly IProductSizeRepo _productSizeRepo;
        private readonly ICategoryProductRepo _categoryProductRepo;
        private readonly IProductStockRepo _productStockRepo;
        private readonly IMapper _mapper;

        public ProductSizeService(IProductSizeRepo productSizeRepo, ICategoryProductRepo categoryProductRepo, IProductStockRepo productStockRepo, IMapper mapper)
        {
            _productSizeRepo = productSizeRepo;
            _categoryProductRepo = categoryProductRepo;
            _productStockRepo = productStockRepo;
            _mapper = mapper;
        }

        public Grid<ProductSizeGridDto> GetProductSize(int pageSize, int pageNumber, string sizeName)
        {
            var oldList = _productSizeRepo.FindAllProductSize(pageSize, pageNumber, sizeName);
            return new Grid<ProductSizeGridDto>
            {
                Count = oldList.Count,
                Data = oldList.Data.ConvertAll(d => _mapper.Map<ProductSizeGridDto>(d)),
                PageNumber = oldList.PageNumber,
                PageSize = oldList.PageSize
            };
        }

        public async Task<ProductSizeInsertDto> InsertProductSize(ProductSizeInsertDto dto)
        {
            var productSize = _mapper.Map<ProductSize>(dto);
            var insertedProductSize = _productSizeRepo.CreateProductSize(productSize);
            var sizeId = insertedProductSize.SizeId;

            var sizeCategory = new SizeCategory { 
                SizeId = sizeId,
                CategoryId = dto.CategoryId
            };

            _productSizeRepo.CreateSizeCategory(sizeCategory);

            return _mapper.Map<ProductSizeInsertDto>(insertedProductSize);
        }
        public async Task<List<DropdownDto>> GetCategoryDropdown()
        {
            return _categoryProductRepo.FindCategoryDropdown();
        }

        public void DeleteProductSize(int sizeId,int categoryId)
        {
            var size = _productStockRepo.FindProductStockBySizeId(sizeId);
            if (size == null)
            {
                _productSizeRepo.DeleteSizeCategory(sizeId, categoryId);
                _productSizeRepo.DeleteProductSize(sizeId);
            }
            else {
                throw new Exception("Size associated with Stock, cannot be deleted");
            }
                
        }

        public async Task<ProductSizeUpdateDto> UpdateProductSize(ProductSizeUpdateDto dto)
        {
            var size = _productSizeRepo.FindProductSizeById(dto.SizeId);
            _mapper.Map(dto, size);
            size = _productSizeRepo.UpdateProductSize(size);
            return _mapper.Map<ProductSizeUpdateDto>(size);
        }


        public async Task<ProductSizeDetailsDto> GetProductSizeById(int sizeId)
        {
            var result = _productSizeRepo.FindProductSizeById(sizeId);
            return _mapper.Map<ProductSizeDetailsDto>(result);
        }


    }
}
