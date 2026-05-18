using QResto.Shared.DTOs.Responses;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace QResto.Blazor.Services
{
    public class AdminService
    {
        private readonly HttpClient _httpClient;
        public AdminService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryResponse>> GetAllCategoriesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<CategoryResponse>>("api/Categories/GetAllCategories") ?? new();
            }
            catch (Exception ex)
            {
                // Gələcəkdə bura Logger əlavə edib xətanı qeyd edə bilərsən
                // Console.WriteLine(ex.Message);
                return new List<CategoryResponse>();
            }
        }
        public async Task<List<TopProductResponse>> GetTopSellingProductsAsync(DateTime startDate, DateTime endDate, int? categoryId)
        {
            // Tarixləri URL-də problem yaratmayacaq standart formata salırıq (məs: 2026-05-19)
            string formattedStart = startDate.ToString("yyyy-MM-dd");
            string formattedEnd = endDate.ToString("yyyy-MM-dd");

            // Əsas URL strukturunu qururuq
            string url = $"api/Reports/GetTopSellingProducts/{formattedStart}/{formattedEnd}";

            // Əgər categoryId seçilibsə və null deyilsə, QueryString olaraq sonuna yapışdırırıq
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                url += $"?categoryId={categoryId.Value}";
            }

            try
            {
                // API-a sorğu atırıq
                var response = await _httpClient.GetFromJsonAsync<List<TopProductResponse>>(url);
                return response ?? new List<TopProductResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TopSellingProducts gətirilərkən xəta baş verdi: {ex.Message}");
                return new List<TopProductResponse>();
            }
        }
    }
}
