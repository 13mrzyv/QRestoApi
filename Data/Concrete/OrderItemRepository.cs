using Data.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete
{
    public class OrderItemRepository : BaseRepository, IOrderItemRepository
    {
        public OrderItemRepository(IDbConnection connection, IDbTransaction transaction)
            : base(connection, transaction) { }
    }
}
