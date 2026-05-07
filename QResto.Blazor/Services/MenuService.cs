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
        //public async Task<List<ProductResponse>> GetMenuAsync()
        //{
        //    // Əgər data artıq çəkilibsə, API-a getmə, yaddaşdakını qaytar
        //    if (CachedProducts != null) return CachedProducts;
        //    //await BasketService.LoadActiveOrder(3);

            //    // Əgər data yoxdursa, API-dan çək və yaddaşa yaz
            //    CachedProducts = await _httpClient.GetFromJsonAsync<List<ProductResponse>>("api/products/GetFullMenu");
            //    return CachedProducts;
            //}

            // TEST ÜÇÜN MOCK DATA (Şablon məlumatlar)
        public async Task<List<ProductResponse>> GetMenuAsync()
        {
            // Real API hissəsini şərhə alırıq
            // if (CachedProducts != null) return CachedProducts;
            // CachedProducts = await _httpClient.GetFromJsonAsync<List<ProductResponse>>("api/products/GetFullMenu");

            // TEST ÜÇÜN MOCK DATA (Şablon məlumatlar)
            if (CachedProducts == null)
            {
                CachedProducts = new List<ProductResponse>
        {
            new ProductResponse
            {
                Id = 1,
                CategoryId = 1,
                CategoryName = "İçkilər",
                Name = "Latte",
                Price = 6.50m,
                Description = "Təzə çəkilmiş qəhvə və südlü köpük.",
                ImageUrl = "https://images.unsplash.com/photo-1541167760496-1628856ab772?q=80&w=200&auto=format&fit=crop",
                IsAvailable = true
            },
            new ProductResponse
            {
                Id = 2,
                CategoryId = 1,
                CategoryName = "İçkilər",
                Name = "Espresso",
                Price = 4.00m,
                Description = "Güclü və zəngin dadlı tək şot qəhvə.",
                ImageUrl = "https://images.unsplash.com/photo-1510707577719-5d6815a0533a?q=80&w=200&auto=format&fit=crop",
                IsAvailable = true
            },
            new ProductResponse
            {
                Id = 3,
                CategoryId = 2,
                CategoryName = "Şirniyyatlar",
                Name = "Çizkeyk",
                Price = 8.50m,
                Description = "Klassik San Sebastian üsulu.",
                ImageUrl = "https://images.unsplash.com/photo-1533134242443-d4fd215305ad?q=80&w=200&auto=format&fit=crop",
                IsAvailable = true
            },
            new ProductResponse
            {
                Id = 4,
                CategoryId = 2,
                CategoryName = "Şirniyyatlar",
                Name = "Tiramisu",
                Price = 7.00m,
                Description = "İtalyan üsulu maskarpone pendiri ilə.",
                ImageUrl = "https://images.unsplash.com/photo-1571877227200-a0d98ea607e9?q=80&w=200&auto=format&fit=crop",
                IsAvailable = true
            }
        };

                // Simulyasiya üçün kiçik bir gecikmə (istəyə bağlı)
                await Task.Delay(500);
            }

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
