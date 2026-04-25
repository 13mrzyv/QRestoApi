using Business.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class CreateOrderRequest
    {
        // Hansı masadan sifariş gəlir?
        public int TableId { get; set; }

        // Sifariş olunan məhsulların siyahısı
        public List<OrderItemRequest> Items { get; set; }
    }
}
