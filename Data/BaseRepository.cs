using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BaseRepository
    {
        protected readonly IDbConnection _connection;
        protected readonly IDbTransaction _transaction;

        public BaseRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }
    }
}
