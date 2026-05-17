using AutoMapper;
using Business.Abstract;
using Business.DTOs.Requests;
using Business.DTOs.Responses;
using Data.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ExpenseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> AddExpenseAsync(CreateExpenseRequest expenseRequest)
        {
            try
            {
                var expense = _mapper.Map<Expense>(expenseRequest);
                var result = await _unitOfWork.ExpensesRepository.AddExpenseAsync(expense);
                _unitOfWork.Commit(); // Əgər transaction istifadə edirsənsə mütləq commit et
                return result > 0;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<ExpenseResponse>> GetExpensesOfTodayAsync()
        {
            var results = await _unitOfWork.ExpensesRepository.GetExpensesOfTodayAsync();
            var expenses = _mapper.Map<IEnumerable<ExpenseResponse>>(results);
            return expenses;
        }
        public async Task<bool> DeleteExpenseByIdAsync(int id)
        {
            try
            {
                var result = await _unitOfWork.ExpensesRepository.DeleteExpenseByIdAsync(id);
                _unitOfWork.Commit(); // Əgər transaction istifadə edirsənsə mütləq commit et
                return result;
            }
            catch
            {
                return false;
            }
        }
    }
}
