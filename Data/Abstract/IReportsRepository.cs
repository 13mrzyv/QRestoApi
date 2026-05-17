using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface IReportsRepository
    {
        Task<dynamic> GetDailyReportAsync(DateTime date);
        Task<dynamic> GetTotalSalesAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<dynamic>> GetTopSellingProductsAsync(DateTime startDate, DateTime endDate, int? categoryId);
    }
}
