using Business.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IReportsService
    {
        Task<DailyReportResponse> GetDailyEarningsAsync(DateTime? date);
        Task<TotalSalesResponse> GetTotalSalesAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TopProductResponse>> GetTopSellingProductsAsync(DateTime startDate, DateTime endDate, int? categoryId);
    }
}
