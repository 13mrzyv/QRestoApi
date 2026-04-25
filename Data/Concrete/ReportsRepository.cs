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
    }
}
