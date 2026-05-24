using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface IExpenseRepository
    {
        Task<int> AddExpenseAsync(Expense expense);
        Task<IEnumerable<Expense>> GetExpensesOfTodayAsync();
        Task<bool> DeleteExpenseByIdAsync(int id);
        Task<decimal> GetTotalExpensesAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Expense>> GetExpensesByDateAsync(DateTime date);
    }
}
