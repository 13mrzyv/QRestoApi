using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Responses
{
    public class OrderDetailResponse
    {
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemResponse> Items { get; set; } // İçindəki məhsullar
    }
}
