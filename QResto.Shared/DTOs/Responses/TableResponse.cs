using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QResto.Shared.DTOs.Responses
{
    public class TableResponse
    {
        public int Id { get; set; }
        public string TableNumber { get; set; }
        public int Status { get; set; }  //0 bos 1 dolu 
        public decimal TotalAmount { get; set; }
    }
}
