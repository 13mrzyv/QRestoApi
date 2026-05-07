using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QResto.Shared.DTOs.Requests
{
    public class OrderItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal  Price { get; set; } // Səbətdəki qiymət, backend-də yoxlanacaq
        public string? Note { get; set; }
    }
}
