using Business.Abstract;
using Business.DTOs.Requests;
using Data.Abstract;
using Data.Concrete;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _unitOfWork.CategoriesRepository.GetAllCategoriesAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _unitOfWork.CategoriesRepository.GetCategoryByIdAsync(id);

        }
        public async Task<bool> InsertCategoryAsync(InsertCategoryRequest insertCategoryRequest)
        {
            var category = new Category
            {
                Name = insertCategoryRequest.Name,
            };
            var affectedRows = await _unitOfWork.CategoriesRepository.InsertCategoryAsync(category);
            return affectedRows > 0;
        }
        public async Task<bool> UpdateCategoryByIdAsync(int id, UpdateCategoryRequest updateCategoryRequest)
        {
            var category = await _unitOfWork.CategoriesRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return false;
            }
            category.Name = updateCategoryRequest.Name;
            category.IsActive = updateCategoryRequest.IsActive;
            var affectedRows = await _unitOfWork.CategoriesRepository.UpdateCategoryAsync(category);
            return affectedRows > 0;
        }
    }
}
