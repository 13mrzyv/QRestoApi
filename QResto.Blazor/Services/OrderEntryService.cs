namespace QResto.Blazor.Services
{
    using QResto.Shared.DTOs.Responses;
    using System.Net.Http.Json;

    public class OrderEntryService
    {
        private readonly HttpClient _http;

        public event Action OnChange;

        // Masanın bazada olan (Mövcud) sifariş detalları
        public OrderDetailResponse ExistingOrder { get; private set; }

        // Ofisiantın indicə sağ menyudan seçdiyi (Yeni) məhsullar
        public List<OrderItemResponse> NewItems { get; private set; } = new();
        // 1. Masanın mövcud sifarişini bazadan yükləyir (Sol orta hissə üçün)
        public OrderEntryService(HttpClient http)
        {
            _http = http; // Əgər bu sətir yoxdursa, _http null qalacaq
        }

        public async Task<bool> UpdateProductStatusAsync(int productId, bool newStatus)
        {
            var response = await _http.PutAsJsonAsync($"api/Products/UpdateStatus/{productId}", newStatus);
            return response.IsSuccessStatusCode;
        }
        public async Task LoadExistingOrderAsync(int tableId)
        {
            try
            {
                // DEBUG: Hansı masaya sorğu getdiyini görək
                Console.WriteLine($"Masa #{tableId} üçün sifariş çəkilir...");

                var response = await _http.GetFromJsonAsync<OrderDetailResponse>($"api/Orders/GetActiveOrderDetails/{tableId}");

                if (response != null)
                {
                    ExistingOrder = response;
                    Console.WriteLine($"Sifariş tapıldı! Məhsul sayı: {ExistingOrder.Items?.Count ?? 0}");
                }
                else
                {
                    Console.WriteLine("API null qaytardı (Bu masada aktiv sifariş yoxdur).");
                    ExistingOrder = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Xətası: {ex.Message}");
                ExistingOrder = null;
            }
            NotifyStateChanged();
        }

        // 2. Sağ menyudan məhsula klikləyəndə "Yeni Sifarişlər" siyahısına əlavə edir
        public void AddToNewItems(ProductResponse product)
        {
            var existing = NewItems.FirstOrDefault(x => x.ProductId == product.Id);

            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                NewItems.Add(new OrderItemResponse
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = 1
                });
            }
            NotifyStateChanged();
        }

        // 3. Yeni sifariş siyahısından məhsulu silir
        public void RemoveFromNewItems(int productId)
        {
            var item = NewItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                if (item.Quantity > 1)
                    item.Quantity--;
                else
                    NewItems.Remove(item);
            }
            NotifyStateChanged();
        }

        // 4. Ümumi məbləği hesablayır (Bazada olan + Yeni seçilənlər)
        public decimal GetTotalAmount()
        {
            decimal existingTotal = ExistingOrder?.TotalAmount ?? 0;
            decimal newTotal = NewItems.Sum(x => x.UnitPrice * x.Quantity);

            return existingTotal + newTotal;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

}