using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Responses
{
    public class DailyReportResponse
    {
        public DateTime Date { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public decimal AverageOrderValue => TotalOrders > 0 ? TotalRevenue / TotalOrders : 0;
    }
}
