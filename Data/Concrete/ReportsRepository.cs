using Dapper;
using Data.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Data.Concrete
{
    public class ReportsRepository : BaseRepository, IReportsRepository
    {
        public ReportsRepository(IDbConnection connection, IDbTransaction transaction)
            : base(connection, transaction) { }

        public async Task<dynamic> GetDailyReportAsync(DateTime date)
        {
            var sql = @"
            SELECT 
                CAST(OrderDate AS DATE) AS [ReportDate], 
                SUM(TotalAmount) AS TotalRevenue, 
                COUNT(Id) AS TotalOrders
            FROM Orders
            WHERE CAST(OrderDate AS DATE) = CAST(@Date AS DATE) 
              AND Status = 2 
            GROUP BY CAST(OrderDate AS DATE)";
            return await _connection.QuerySingleOrDefaultAsync<dynamic>(sql, new { Date = date }, _transaction);

        }
        public async Task<dynamic> GetTotalSalesAsync(DateTime startDate, DateTime endDate)
        {
            var sql = @"
            SELECT 
                ISNULL(SUM(CASE WHEN Status = 2 THEN TotalAmount ELSE 0 END), 0) AS CompletedTotal,
                ISNULL(SUM(CASE WHEN Status = 1 THEN TotalAmount ELSE 0 END), 0) AS ActiveTotal
            FROM Orders
            WHERE CAST(OrderDate AS DATE) BETWEEN CAST(@StartDate AS DATE) AND CAST(@EndDate AS DATE)";

            // Dapper parametr olaraq DateTime-ı özü düzgün formata salacaq
            return await _connection.QueryFirstOrDefaultAsync<dynamic>(sql, new { StartDate = startDate, EndDate = endDate }, _transaction);
        }
        public async Task<IEnumerable<dynamic>> GetTopSellingProductsAsync(DateTime startDate, DateTime endDate, int? categoryId)
        {
            var sql = @"SELECT 
                p.Name AS ProductName,
                SUM(oi.Quantity) AS SalesCount,
                SUM(oi.Quantity * oi.UnitPrice) AS TotalRevenue
            FROM OrderItems oi
            INNER JOIN Products p ON oi.ProductId = p.Id
            INNER JOIN Orders o ON oi.OrderId = o.Id
            WHERE o.OrderDate >= @StartDate 
              AND o.OrderDate <= @EndDate
              AND (@CategoryId IS NULL OR p.CategoryId = @CategoryId)
            GROUP BY p.Id, p.Name
            ORDER BY SalesCount DESC;";
            return await _connection.QueryAsync<dynamic>(sql, new { StartDate = startDate, EndDate = endDate, CategoryId = categoryId }, _transaction);
        }
    }
}
