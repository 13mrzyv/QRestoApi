using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QResto.Shared.DTOs.Responses
{
    public class OrderItemResponse
    {
        public int ProductId { get; set; } // OrderItem ID-si, lazım ola bilər
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
