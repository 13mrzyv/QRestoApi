using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class KitchenTicketModel
    {
        public string TableName { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderTime { get; set; }
        public List<KitchenItem> Items { get; set; }
    }
}
