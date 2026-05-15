using Business.Abstract;
using Business.DTOs.Responses;
using Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ReportsService : IReportsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DailyReportResponse> GetDailyEarningsAsync(DateTime? date)
        {
            var targetDate = date?.Date ?? DateTime.UtcNow.Date;
            var result = await _unitOfWork.ReportsRepository.GetDailyReportAsync(targetDate);
            if (result == null)
            {
                return new DailyReportResponse
                {
                    Date = targetDate,
                    TotalRevenue = 0,
                    TotalOrders = 0
                };
            }
            return new DailyReportResponse
            {
                Date = result.ReportDate ?? targetDate,
                TotalRevenue = result.TotalRevenue ?? 0m,
                TotalOrders = result.TotalOrders ?? 0
            };
        }
        public async Task<TotalSalesResponse> GetTotalSalesAsync(DateTime startDate, DateTime endDate)
        {
            var result = await _unitOfWork.ReportsRepository.GetTotalSalesAsync(startDate, endDate);
            return new TotalSalesResponse
            {
                CompletedTotal = result.CompletedTotal ?? 0m,
                ActiveTotal = result.ActiveTotal ?? 0m
            };
        }
    }
}
