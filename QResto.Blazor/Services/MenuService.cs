using QResto.Shared.DTOs.Responses;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;


namespace QResto.Blazor.Services
{
    public class MenuService
    {
        private readonly HttpClient _httpClient;
        // Datanı burada yaddaşda saxlayırıq
        public List<ProductResponse> CachedProducts { get; private set; }
        public MenuService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ProductResponse>> GetMenuAsync()
        {
            // Əgər data artıq çəkilibsə, API-a getmə, yaddaşdakını qaytar
            if (CachedProducts != null) return CachedProducts;
            //await BasketService.LoadActiveOrder(3);

            // Əgər data yoxdursa, API-dan çək və yaddaşa yaz
            CachedProducts = await _httpClient.GetFromJsonAsync<List<ProductResponse>>("api/products/GetFullMenu");
            return CachedProducts;
        }

        // Hesabı bağlamaq (Ödəniş etmək) metodu
        public async Task<bool> CloseTableAccount(int tableId)
        {
            try
            {
                // Backend-də yazdığımız "api/Orders/CloseAccount/{tableId}" endpoint-inə POST sorğusu göndəririk
                // Əgər data göndərmiriksə, ikinci parametri null qoya bilərik
                var response = await _httpClient.PutAsync($"api/Orders/Checkout/{tableId}", null);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"{tableId} nömrəli masanın hesabı bağlandı.");
                    return true;
                }

                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Xəta baş verdi: {error}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bağlantı xətası: {ex.Message}");
                return false;
            }
        }
    }
}
