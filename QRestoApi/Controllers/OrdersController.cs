using Business.Abstract;
using Business.DTOs;
using Business.DTOs.Requests;
using Entity;
using Microsoft.AspNetCore.Mvc;

namespace QRestoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            await _orderService.PlaceOrderAsync(request);

            return Ok("Sifariş qəbul edildi və emal olunur.");
        }

        [HttpGet("{tableId}")]
        public async Task<IActionResult> GetActiveOrderDetails(int tableId)
        {
            var order = await _orderService.GetOrderDetailsAsync(tableId);

            if (order == null)
                return NotFound("Sifariş tapılmadı.");

            return Ok(order);
        }
        [HttpPut("{tableId}")]
        public async Task<IActionResult> Checkout(int tableId)
        {
            await _orderService.CheckoutAsync(tableId);
            return Ok("Hesab bağlandı, masa artıq boşdur.");
        }
        //[HttpGet]
        //public async Task<IActionResult> GetActiveOrderItemsByTable(int tableId)
        //{
        //    var items = await _orderService.GetActiveOrderIdByTableIdAsync(tableId);
        //    return Ok(items);
        //}
    }
}
