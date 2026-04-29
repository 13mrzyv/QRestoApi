using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QResto.Shared.DTOs.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public int CategoryId { get; set; } // SQL-dəki Foreign Key
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public bool IsAvailable { get; set; }   
    }
}
