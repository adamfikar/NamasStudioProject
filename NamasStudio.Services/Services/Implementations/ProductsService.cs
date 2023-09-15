using AutoMapper;
using NamasStudio.DataAccess.Models;
using NamasStudio.Dto;
using NamasStudio.Dto.Product;
using NamasStudio.Repository.Repositories.Interfaces;
using NamasStudio.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Services.Services.Implementations
{
    internal class ProductsService : IProductsService
    {
        private readonly IProductsRepo _productRepo;
        private readonly ICategoryProductRepo _categoryProductRepo;
        private readonly IMapper _mapper;

        public ProductsService(IProductsRepo productRepo, ICategoryProductRepo categoryProductRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _categoryProductRepo = categoryProductRepo;
            _mapper = mapper;
        }

        public Grid<ProductGridDto> GetProducts(int pageSize, int pageNumber, string product, int? categoryId)
        {
            var list = _productRepo.FindAllProducts(pageSize, pageNumber, product, categoryId);
            return new Grid<ProductGridDto>
            {
                Count = list.Count,
                Data = list.Data.ConvertAll(d => _mapper.Map<ProductGridDto>(d)),
                PageNumber = list.PageNumber,
                PageSize = list.PageSize
            };
        }

        public List<DropdownDto> GetCategoryDropdown()
        {
            return _categoryProductRepo.FindCategoryDropdown();
        }

        public ProductInsertDto InsertProduct(ProductInsertDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            _productRepo.CreateProduct(product);
            return _mapper.Map<ProductInsertDto>(product);
        }

        public void DeleteProduct(int productId)
        {
            _productRepo.DeleteProduct(productId);
        }

        public ProductUpdateDto GetProductById(int productId)
        {
            var result = _productRepo.FindProductById(productId);
            return _mapper.Map<ProductUpdateDto>(result);
        }

        public ProductUpdateDto UpdateProduct(ProductUpdateDto dto)
        {
            var product = _productRepo.FindProductById(dto.ProductId);
            _mapper.Map(dto, product);
            product = _productRepo.UpdateProduct(product);
            return _mapper.Map<ProductUpdateDto>(product);
        }

    }
}
