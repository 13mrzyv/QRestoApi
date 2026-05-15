using Dapper;
using Data.Abstract;
using Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data.Concrete
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(IDbConnection connection, IDbTransaction transaction)
            : base(connection, transaction) { }

        public async Task<int> InsertOrderAsync(Order order)
        {
            var sql = @"INSERT INTO Orders (TableId, OrderDate, TotalAmount, Status) 
                   VALUES (@TableId, @OrderDate, @TotalAmount, @Status);
                   SELECT CAST(SCOPE_IDENTITY() as int);";

            return await _connection.QuerySingleAsync<int>(sql, order, _transaction);
        }

        public async Task InsertOrderItemsAsync(List<OrderItem> items)
        {
            var sql = @"INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice, Note) 
                   VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice, @Note)";

            await _connection.ExecuteAsync(sql, items, _transaction);
        }
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var sql = @"SELECT * FROM Orders WHERE Id = @Id";
            return await _connection.QuerySingleOrDefaultAsync<Order>(sql, new { Id = id }, _transaction);
        }
        public async Task<IEnumerable<dynamic>> GetOrderItemsByOrderIdAsync(int id)
        {
            var sql = @"SELECT  p.Id AS ProductId, p.Name, oi.Quantity, oi.UnitPrice, oi.OrderDate, oi.Note
                FROM OrderItems oi
                JOIN Products p ON oi.ProductId = p.Id
                WHERE oi.OrderId = @OrderId";

            return await _connection.QueryAsync(sql, new { OrderId = id }, _transaction);
        }
        public async Task<Order> GetActiveOrderByTableIdAsync(int tableId)
        {
            var sql = "SELECT * FROM Orders WHERE TableId = @TableId AND Status = 1";
            return await _connection.QuerySingleOrDefaultAsync<Order>(sql, new { TableId = tableId }, _transaction);
        }
        public async Task UpdateOrderStatusAsync(int orderId, int newStatus)
        {
            var sql = "UPDATE Orders SET Status = @Status WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Status = newStatus, Id = orderId }, _transaction);

        }
        // isletmedim belke

        public async Task UpdateTotalAmountByIdAsync(int orderId, decimal newTotalAmount)
        {
            var sql = "UPDATE Orders SET TotalAmount = @TotalAmount WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { TotalAmount = newTotalAmount, Id = orderId }, _transaction);
        }
        public async Task<IEnumerable<OrderWithTableNumber>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var sql = @"
        SELECT o.*, t.TableNumber as TableNumber 
        FROM Orders o
        JOIN Tables t ON o.TableId = t.Id
        WHERE CAST(o.OrderDate AS DATE) BETWEEN CAST(@StartDate AS DATE) AND CAST(@EndDate AS DATE)
        ORDER BY o.Id DESC";

            return await _connection.QueryAsync<OrderWithTableNumber>(sql, new
            {
                StartDate = startDate,
                EndDate = endDate
            }, transaction: _transaction);
        }

        public async Task<int> NumberOfActiveOrdersAsync()
        {
            var sql = "SELECT COUNT(*) FROM Orders WHERE Status = 1;";
            return await _connection.QuerySingleAsync<int>(sql, transaction: _transaction);
        }
    }
}
