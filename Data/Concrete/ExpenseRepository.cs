using Dapper;
using Data.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete
{
    public class ExpenseRepository : BaseRepository, IExpenseRepository
    {
        public ExpenseRepository(IDbConnection connection, IDbTransaction transaction)
            : base(connection, transaction) { }


        public async Task<int> AddExpenseAsync(Expense expense)
        {
            var sql = "INSERT INTO Expenses (Description, Amount) VALUES (@Description, @Amount)";

            return await _connection.ExecuteAsync(sql, expense, transaction: _transaction);
        }
    }
}
