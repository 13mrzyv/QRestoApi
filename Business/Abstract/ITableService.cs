using Business.DTOs.Requests;
using Business.DTOs.Responses;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<IEnumerable<TableResponse>> GetAllTableResponsesAsync();
        Task CreateTableAsync(InsertTableRequest insertTableRequest);
        Task UpdateTableStatusAsync(int id, int status);
        Task <string> GetTableNumberByTableIdAsync(int tableId);

    }
}
