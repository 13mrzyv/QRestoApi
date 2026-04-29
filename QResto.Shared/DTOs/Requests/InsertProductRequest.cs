using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QResto.Shared.DTOs.Requests
{
    public class InsertProductRequest
    {
        public int CategoryId { get; set; } // Hansı kateqoriyaya aid olduğunu mütləq bilməliyik
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; } // Yeni məhsul bəlkə hələ hazır deyil, susmaya görə (default) true verə bilərsən
    }
}
