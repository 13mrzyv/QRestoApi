using Business.DTOs.Requests;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<bool> InsertCategoryAsync(InsertCategoryRequest insertCategoryRequest);
        Task<bool> UpdateCategoryByIdAsync(int id, UpdateCategoryRequest updateCategoryRequest);
        
    }
}
