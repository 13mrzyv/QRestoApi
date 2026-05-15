using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QResto.Shared.DTOs.Responses
{
    public class OrderSummaryResponse
    {
        public int Id { get; set; }
        public string TableNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }  // 1: Yeni 2: Ödənildi (Arxiv) 
    }
}
