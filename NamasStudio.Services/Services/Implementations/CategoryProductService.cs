using AutoMapper;
using NamasStudio.DataAccess.Models;
using NamasStudio.Dto;
using NamasStudio.Dto.Category;
using NamasStudio.Repository.Repositories.Interfaces;
using NamasStudio.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NamasStudio.Services.Services.Implementations
{
    internal class CategoryProductService : ICategoryProductService
    {
        private readonly ICategoryProductRepo _categoryProductRepo;
        private readonly IMapper _mapper;

        public CategoryProductService(ICategoryProductRepo categoryProductRepo, IMapper mapper)
        {
            _categoryProductRepo = categoryProductRepo;
            _mapper = mapper;
        }

        public Grid<CategoryProductGridDto> GetCategory(int pageSize, int pageNumber, string? category)
        {
            var oldList = _categoryProductRepo.FindAllCategoryProducts(pageSize, pageNumber, category);
            return new Grid<CategoryProductGridDto>
            {
                Count = oldList.Count,
                Data = oldList.Data.ConvertAll(d => _mapper.Map<CategoryProductGridDto>(d)),
                PageNumber = oldList.PageNumber,
                PageSize = oldList.PageSize
            };
        }

        public async Task<CategoryProductInsertDto> InsertCategory(CategoryProductInsertDto dto)
        {
            var category = _mapper.Map<CategoryProduct>(dto);
            _categoryProductRepo.CreateCategoryProduct(category);
            return _mapper.Map<CategoryProductInsertDto>(category);
        }

        public void DeleteCategory(int categoryId) {
            _categoryProductRepo.DeleteCategoryProduct(categoryId);
        }

        public async Task<CategoryProductDetailsDto> GetCategoryById(long id)
        {
            var result = _categoryProductRepo.FindCategoryProductById(id);
            return _mapper.Map<CategoryProductDetailsDto>(result);
        }

        public async Task<CategoryProductUpdateDto> UpdateCategory(CategoryProductUpdateDto dto)
        {
            var category = _categoryProductRepo.FindCategoryProductById(dto.CategoryId);
            _mapper.Map(dto, category);
            category = _categoryProductRepo.UpdateCategoryProduct(category);
            return _mapper.Map<CategoryProductUpdateDto>(category);
        }

      
    }
}
