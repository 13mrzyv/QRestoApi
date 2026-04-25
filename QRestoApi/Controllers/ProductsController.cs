using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Business.DTOs.Requests;

namespace QRestoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryIdAsync(categoryId);
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> InsertProduct(InsertProductRequest insertProductRequest)
        {
            var result = await _productService.InsertProductAsync(insertProductRequest);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProductById(int id, InsertProductRequest insertProductRequest)
        {
            var result = await _productService.UpdateProductByIdAsync(id, insertProductRequest);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            var result = await _productService.DeleteProductByIdAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
