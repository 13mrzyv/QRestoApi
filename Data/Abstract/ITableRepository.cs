using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task InsertTableAsync(Table table);
        Task UpdateTableStatusAsync(int id, int status);
        Task<int> GetTableStatusAsync(int id);
    }
}
