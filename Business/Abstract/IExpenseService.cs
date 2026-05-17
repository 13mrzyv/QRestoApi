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
    public interface IExpenseService
    {
        Task<bool> AddExpenseAsync(CreateExpenseRequest expenseRequest);
        Task<IEnumerable<ExpenseResponse>> GetExpensesOfTodayAsync();
        Task<bool> DeleteExpenseByIdAsync(int id);
    }
}
