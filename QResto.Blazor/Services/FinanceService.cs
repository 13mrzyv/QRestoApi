using QResto.Shared.DTOs.Requests;
using QResto.Shared.DTOs.Responses;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace QResto.Blazor.Services
{
    public class FinanceService
    {
        private readonly HttpClient _httpClient;
        public FinanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ExpenseResponse>> GetTodayExpensesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ExpenseResponse>>("api/expense/GetExpensesOfToday");
                return response ?? new List<ExpenseResponse>();
            }
            catch (Exception ex)
            {
                // Xətanı mərkəzi loqlamaq və ya idarə etmək üçün əla yerdir
                Console.WriteLine($"FinanceService Error: {ex.Message}");
                return new List<ExpenseResponse>();
            }
        }
        public async Task<bool> AddExpenseAsync(CreateExpenseRequest expenseRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/expense/AddExpense", expenseRequest);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Xətanı mərkəzi loqlamaq və ya idarə etmək üçün əla yerdir
                Console.WriteLine($"FinanceService Error: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteExpenseAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/expense/DeleteExpenseById/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Xətanı mərkəzi loqlamaq və ya idarə etmək üçün əla yerdir
                Console.WriteLine($"FinanceService Error: {ex.Message}");
                return false;
            }
        }
    }
}
