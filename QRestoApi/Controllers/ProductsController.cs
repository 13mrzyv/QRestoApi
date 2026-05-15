using Business.Abstract;
using Business.DTOs.Requests;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace QRestoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IHubContext<OrderHub> _hubContext;

        public ProductsController(IProductService productService, IHubContext<OrderHub> hubContext)
        {
            _productService = productService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductsForCustomer()
        {
            var products = await _productService.GetAllProductsForCustomerAsync();
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

        [HttpGet]
        public async Task<IActionResult> GetFullMenu()
        {
            var products = await _productService.GetFullMenuAsync();
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] bool isAvailable)
        {
            var result = await _productService.UpdateProductStatusAsync(id, isAvailable);

            if (!result)
            {
                return NotFound(new { Message = "Məhsul tapılmadı və ya status dəyişdirilmədi." });
            }

            await _hubContext.Clients.All.SendAsync("ReceiveStockUpdate", id, isAvailable);

            return Ok();
        }


    }
}
