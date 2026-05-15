using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class OrderWithTableNumber: Order // Order-dən miras alır (bütün sütunlar gəlir)
    {
        public string TableNumber { get; set; } // SQL-dən gələn masa adını bura qoyacağıq
    }
}
