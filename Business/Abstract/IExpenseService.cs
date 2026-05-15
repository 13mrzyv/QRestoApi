using Business.DTOs.Requests;
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
    }
}
