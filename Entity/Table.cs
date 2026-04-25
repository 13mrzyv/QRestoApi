using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Table
    {
        public int Id { get; set; }
        public string TableNumber { get; set; }
        public int Status { get; set; } // 0: Boş, 1: Dolu
        public string QRToken { get; set; }
    }
}
