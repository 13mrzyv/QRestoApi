using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QResto.Shared.DTOs.Requests
{
    public class CreateExpenseRequest
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
