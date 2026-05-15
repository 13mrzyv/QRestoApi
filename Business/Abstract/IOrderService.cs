using Business.DTOs;
using Business.DTOs.Requests;
using Business.DTOs.Responses;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        Task PlaceOrderAsync(CreateOrderRequest request);
        Task<OrderDetailResponse> GetActiveOrderDetailsAsync(int tableId);
        Task CheckoutAsync(int tableId);
        Task<Order> GetActiveOrderIdByTableIdAsync(int tableId);
        Task<IEnumerable<OrderSummaryResponse>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<int> NumberOfActiveOrdersAsync();
        Task<OrderDetailResponse> GetOrderDetailsByIdAsync(int orderId);
    }
}
