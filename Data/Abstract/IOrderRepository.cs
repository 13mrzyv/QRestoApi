using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface IOrderRepository
    {
        Task<int> InsertOrderAsync(Order order);
        Task InsertOrderItemsAsync(List<OrderItem> items);
        Task<Order> GetOrderByIdAsync(int id);
        Task<IEnumerable<dynamic>> GetOrderItemsByOrderIdAsync(int id);
        public Task UpdateOrderStatusAsync(int orderId,int newStatus);
        Task<Order?> GetActiveOrderByTableIdAsync(int tableId);
        Task UpdateTotalAmountByIdAsync(int orderId, decimal newTotalAmount);
        Task<IEnumerable<OrderWithTableNumber>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<int> NumberOfActiveOrdersAsync();
    }
}
