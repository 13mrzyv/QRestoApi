using AutoMapper;
using Business.Abstract;
using Business.DTOs.Requests;
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
    }
}
