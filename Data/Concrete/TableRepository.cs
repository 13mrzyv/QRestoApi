using Dapper;
using Data.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Data.Concrete
{
    public class TableRepository : BaseRepository, ITableRepository
    {
        public TableRepository(IDbConnection connection, IDbTransaction transaction)
            : base(connection, transaction) { }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            var sql = "SELECT * FROM Tables";

            return await _connection.QueryAsync<Table>(sql, transaction: _transaction);
        }
        public async Task<IEnumerable<dynamic>> GetAllTablesDynamicAsync()
        {
            // View-dan hər şeyi çəkirik
            var sql = "SELECT * FROM View_TableStatus";

            // Tip təyin etmirik, Dapper avtomatik dynamic obyektlər yaradır
            return await _connection.QueryAsync(sql, transaction: _transaction);
        }
        public async Task InsertTableAsync(Table table)
        {
            var sql = "INSERT INTO Tables (TableNumber, Status, QRToken) VALUES (@TableNumber, @Status, @QRToken)";
            await _connection.ExecuteAsync(sql, new { TableNumber = table.TableNumber, Status = table.Status, QRToken = table.QRToken }, transaction: _transaction);
        }
        public async Task UpdateTableStatusAsync(int id, int status)
        {
            var sql = "UPDATE Tables SET Status = @Status WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, new { Status = status, Id = id }, transaction: _transaction);
        }
        public async Task<int> GetTableStatusAsync(int id)
        {
            var sql = "SELECT Status FROM Tables WHERE Id = @Id";
            return await _connection.QuerySingleOrDefaultAsync<int>(sql, new { Id = id }, transaction: _transaction);
        }
    }
}
