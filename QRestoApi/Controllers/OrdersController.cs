using Business.Abstract;
using Business.Abstract.Print;
using Business.Concrete;
using Business.DTOs;
using Business.DTOs.Requests;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace QRestoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IHubContext<OrderHub> _hubContext;
        private readonly IPrintPrepService _printPrepService;
        private readonly IPrintQueue _printQueue;

        public OrdersController(IOrderService orderService, IHubContext<OrderHub> hubContext, IPrintPrepService printPrepService, IPrintQueue printQueue)
        {
            _orderService = orderService;
            _hubContext = hubContext;
            _printPrepService = printPrepService;
            _printQueue = printQueue;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            await _orderService.PlaceOrderAsync(request);

            await _hubContext.Clients.All.SendAsync("ReceiveOrderUpdate");

            //await _printPrepService.PrepareAndPrintKitchenTicketAsync(request);
            // 3. Sifarişi çap növbəsinə atırıq
            // Artıq "await _printPrepService..." yazmırıq!
            // Bu əməliyyat nanosaniyələr ərzində bitir.
            await _printQueue.EnqueuePrintJobAsync(request);


            return Ok("Sifariş qəbul edildi və emal olunur.");
        }

        [HttpGet("{tableId}")]
        public async Task<IActionResult> GetActiveOrderDetails(int tableId)
        {
            var order = await _orderService.GetActiveOrderDetailsAsync(tableId);

            if (order == null)
                return NotFound("Sifariş tapılmadı.");

            return Ok(order);
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetailsById(int orderId)
        {
            var orderDetails = await _orderService.GetOrderDetailsByIdAsync(orderId);

            if (orderDetails == null)
                return NotFound("Sifariş tapılmadı.");

            return Ok(orderDetails);
        }
        [HttpPut("{tableId}")]
        public async Task<IActionResult> Checkout(int tableId)
        {
            await _orderService.CheckoutAsync(tableId);
            await _hubContext.Clients.All.SendAsync("ReceiveOrderUpdate");
            return Ok("Hesab bağlandı, masa artıq boşdur.");
        }
        [HttpGet("{startDate}/{endDate}")]
        public async Task<IActionResult> GetOrdersByRange(DateTime startDate, DateTime endDate)
        {
            var orders = await _orderService.GetOrdersByDateRangeAsync(startDate, endDate);
            return Ok(orders);
        }
        [HttpGet]
        public async Task<IActionResult> NumberOfActiveOrders()
        {
            var result = await _orderService.NumberOfActiveOrdersAsync();
            return Ok(result);
        }
    }
}
