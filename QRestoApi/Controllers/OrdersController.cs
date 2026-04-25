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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetails(int id)
        {
            var order = await _orderService.GetOrderDetailsAsync(id);

            if (order == null)
                return NotFound("Sifariş tapılmadı.");

            return Ok(order);
        }
        [HttpPut]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            await _orderService.CheckoutAsync(request);
            return Ok("Hesab bağlandı, masa artıq boşdur.");
        }
    }
}
