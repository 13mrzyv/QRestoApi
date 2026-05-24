using AutoMapper;
using Business.Abstract;
using Business.DTOs.Requests;
using Business.DTOs.Responses;
using Data.Abstract;
using Data.Concrete;
using Entity;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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

        public async Task<IEnumerable<ExpenseResponse>> GetExpensesByDateAsync(DateTime date)
        {
            try
            {
                var result = await _unitOfWork.ExpensesRepository.GetExpensesByDateAsync(date);
                var expenses = _mapper.Map<IEnumerable<ExpenseResponse>>(result);
                _unitOfWork.Commit();
                return expenses;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetExpensesByDate xətası: {ex.Message}");
                return Enumerable.Empty<ExpenseResponse>();
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
        public async Task<TotalExpensesResponse> GetTotalExpensesAsync(DateTime startDate, DateTime endDate)
        {
            var result = await _unitOfWork.ExpensesRepository.GetTotalExpensesAsync(startDate, endDate);
            return new TotalExpensesResponse
            {
                TotalExpenses = result
            };
        }
    }
}
