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
        public async Task<IEnumerable<Expense>> GetExpensesOfTodayAsync()
        {
            var sql = "SELECT * FROM Expenses WHERE CAST(ExpenseDate AS DATE) = CAST(GETDATE() AS DATE)";

            return await _connection.QueryAsync<Expense>(sql, transaction: _transaction);
        }
        public async Task<bool> DeleteExpenseByIdAsync(int id)
        {
            var sql = "DELETE FROM Expenses WHERE Id = @Id";
            var affectedRows = await _connection.ExecuteAsync(sql, new { Id = id }, transaction: _transaction);
            return affectedRows > 0;
        }
        public async Task<decimal> GetTotalExpensesAsync(DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT ISNULL(SUM(Amount), 0) FROM Expenses WHERE ExpenseDate >= @StartDate AND ExpenseDate < @EndDate";
            return await _connection.ExecuteScalarAsync<decimal>(sql,new { startDate = startDate.Date, endDate = endDate.Date.AddDays(1) },transaction: _transaction);
        }
        public async Task<IEnumerable<Expense>> GetExpensesByDateAsync(DateTime date)
        {
            var sql = @"
        SELECT 
            Id,
            Description,
            Amount,
            ExpenseDate
        FROM Expenses
        WHERE CAST(ExpenseDate AS DATE) = @Date
        ORDER BY ExpenseDate DESC";

            return await _connection.QueryAsync<Expense>(
                sql,
                new { Date = date.Date },
                transaction: _transaction);
        }
    }
}
