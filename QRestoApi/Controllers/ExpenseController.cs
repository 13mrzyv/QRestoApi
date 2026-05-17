using Business.Abstract;
using Business.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace QRestoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost]
        public async Task<IActionResult> AddExpense(CreateExpenseRequest expenseRequest)
        {
            var result = await _expenseService.AddExpenseAsync(expenseRequest);
            if (!result)
            {
                return BadRequest("Xərc əlavə edilərkən bir xəta baş verdi.");
            }
            return Ok("Xərc uğurla əlavə edildi.");
        }
        [HttpGet]
        public async Task<IActionResult> GetExpensesOfToday()
        {
            var expenses = await _expenseService.GetExpensesOfTodayAsync();
            return Ok(expenses);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseById(int id)
        {
            var result = await _expenseService.DeleteExpenseByIdAsync(id);
            if (!result)
            {
                return BadRequest("Xərc silinərkən bir xəta baş verdi.");
            }
            return Ok("Xərc uğurla silindi.");
        }
    }
}
