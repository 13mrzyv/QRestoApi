using QResto.Shared.DTOs.Responses;
using System.Net.Http;
using System.Net.Http.Json;

namespace QResto.Blazor.Services
{
    public class OrderService
    {
        private readonly HttpClient _http;

        public OrderService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<OrderSummaryResponse>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            // Backend-də yazdığın endpoint-in ünvanı
            var start = startDate.ToString("yyyy-MM-dd");
            var end = endDate.ToString("yyyy-MM-dd");
            return await _http.GetFromJsonAsync<List<OrderSummaryResponse>>($"api/Orders/GetOrdersByRange/{start}/{end}");
        }
        public async Task<int> NumberOfActiveOrder()
        {
            return await _http.GetFromJsonAsync<int>("api/Orders/NumberOfActiveOrders");
            
        }
        public async Task<OrderDetailResponse> GetOrderDetailsAsync(int orderId)
        {
            // API ünvanına uyğun olaraq ID-ni parametr kimi göndəririk
            return await _http.GetFromJsonAsync<OrderDetailResponse>($"api/Orders/GetOrderDetailsById/{orderId}");
        }
        public async Task<TotalSalesResponse> GetTotalSalesAsync(DateTime startDate, DateTime endDate)
        {
            // Hər iki tarixi ISO standartına (yyyy-MM-dd) uyğun formatlayırıq
            var start = startDate.ToString("yyyy-MM-dd");
            var end = endDate.ToString("yyyy-MM-dd");

            // API-a hər iki parametri ötürürük
            return await _http.GetFromJsonAsync<TotalSalesResponse>($"api/reports/GetTotalSales/{start}/{end}");
        }

    }
}
