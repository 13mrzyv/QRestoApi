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
            },
            new ProductResponse
            {
                Id = 3,
                CategoryId = 1,
                CategoryName = "İçkilər",
                Name = "Soyuq Americano",
                Price = 5.50m,
                Description = "Buzlu su və təzə espresso qarışığı.",
                ImageUrl = "https://images.unsplash.com/photo-1517701604599-bb29b565090c?q=80&w=200&auto=format&fit=crop",
                IsAvailable = true
            },
            new ProductResponse
            {
                Id = 4,
                CategoryId = 2,
                CategoryName = "Qəlyanaltılar",
                Name = "Sezar Salatı",
                Price = 12.00m,
                Description = "Toyuq filesi, parmesan pendiri və xüsusi sous ilə.",
                ImageUrl = "https://images.unsplash.com/photo-1550304943-4f24f54ddde9?q=80&w=200&auto=format&fit=crop",
                IsAvailable = true
            },
            new ProductResponse
            {
                Id = 5,
                CategoryId = 2,
                CategoryName = "Qəlyanaltılar",
                Name = "Klub Sendviç",
                Price = 9.50m,
                Description = "Qızardılmış çörək, vetçina, yumurta və kahı.",
                ImageUrl = "https://images.unsplash.com/photo-1528735602780-2552fd46c7af?q=80&w=200&auto=format&fit=crop",
                IsAvailable = true
            },
            new ProductResponse
            {
                Id = 6,
                CategoryId = 3,
                CategoryName = "Yeməklər",
                Name = "Çizburqer",
                Price = 11.00m,
                Description = "100% mal əti, çedar pendiri və karamelizə olunmuş soğan.",
                ImageUrl = "https://images.unsplash.com/photo-1568901346375-23c9450c58cd?q=80&w=200&auto=format&fit=crop",
                IsAvailable = true
            },
            new ProductResponse
            {
                Id = 7,
                CategoryId = 3,
                CategoryName = "Yeməklər",
                Name = "Margarita Pizza",
                Price = 14.50m,
                Description = "Mozzarella pendiri, pomidor sousu və təzə reyhan.",
                ImageUrl = "https://images.unsplash.com/photo-1574071318508-1cdbad80ad50?q=80&w=200&auto=format&fit=crop",
                IsAvailable = true
            },
            new ProductResponse
            {
                Id = 8,
                CategoryId = 4,
                CategoryName = "Şirniyyatlar",
                Name = "San Sebastian",
                Price = 8.50m,
                Description = "Məşhur yanmış çizkeyk, şokolad sousu ilə.",
                ImageUrl = "https://images.unsplash.com/photo-1533134242443-d4fd215305ad?q=80&w=200&auto=format&fit=crop",
                IsAvailable = true
            },
            new ProductResponse
            {
                Id = 9,
                CategoryId = 4,
                CategoryName = "Şirniyyatlar",
                Name = "Brauni",
                Price = 7.00m,
                Description = "Bol şokoladlı və qozlu ev üsulu brauni.",
                ImageUrl = "https://images.unsplash.com/photo-1606312619070-d48b4c6c23c2?q=80&w=200&auto=format&fit=crop",
                IsAvailable = true
            },
            new ProductResponse
            {
                Id = 10,
                CategoryId = 1,
                CategoryName = "İçkilər",
                Name = "Yaşıl Çay",
                Price = 3.50m,
                Description = "Antioksidantlarla zəngin təbii yarpaq çayı.",
                ImageUrl = "https://images.unsplash.com/photo-1627435601361-ec25f5b1d0e5?q=80&w=200&auto=format&fit=crop",
                IsAvailable = true
            },
            new ProductResponse
            {
                Id = 11,
                CategoryId = 3,
                CategoryName = "Yeməklər",
                Name = "Fettuccine Alfredo",
                Price = 13.00m,
                Description = "Qaymaqlı sousda göbələk və toyuq ilə pasta.",
                ImageUrl = "https://images.unsplash.com/photo-1645112481338-358aa27db381?q=80&w=200&auto=format&fit=crop",
                IsAvailable = true
            },
            new ProductResponse
            {
                Id = 12,
                CategoryId = 2,
                CategoryName = "Qəlyanaltılar",
                Name = "Fri Kartofu",
                Price = 4.50m,
                Description = "Qızılı rəngdə qızardılmış xırtıldayan kartoflar.",
                ImageUrl = "https://images.unsplash.com/photo-1573080496219-bb080dd4f877?q=80&w=200&auto=format&fit=crop",
                IsAvailable = false // Test üçün birini "Stokda yoxdur" edək
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
