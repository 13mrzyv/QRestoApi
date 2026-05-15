using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QResto.Shared.DTOs.Responses
{
    public class TotalSalesResponse
    {
        public decimal CompletedTotal { get; set; } // Status 2 olanlar
        public decimal ActiveTotal { get; set; }    // Status 1 olanlar
    }
}
