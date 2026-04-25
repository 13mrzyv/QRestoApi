using Business.Abstract;
using Business.DTOs.Requests;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QRestoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> InsertCategory(InsertCategoryRequest insertCategoryRequest)
        {
            var result = await _categoryService.InsertCategoryAsync(insertCategoryRequest);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCategoryById(int id ,UpdateCategoryRequest updateCategoryRequest)
        {
            var result = await _categoryService.UpdateCategoryByIdAsync(id,updateCategoryRequest);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
