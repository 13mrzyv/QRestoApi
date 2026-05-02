namespace QResto.Blazor.Services
{
    using QResto.Shared.DTOs;
    using QResto.Shared.DTOs.Requests;
    using QResto.Shared.DTOs.Responses;
    using System.Net.Http.Json;

    public class BasketService
    {
        private readonly HttpClient _http;
        private readonly OrderEntryService orderService;
        public event Action OnChange;
        public List<OrderItemResponse> Items { get; private set; } = new();
        public List<OrderItemResponse> OrderedItems { get; private set; } = new();

        public BasketService(HttpClient http, OrderEntryService orderService)
        {
            _http = http;
            this.orderService = orderService;
        }   
        public void AddToBasket(ProductResponse product)
        {
            var existing = Items.FirstOrDefault(x => x.ProductId == product.Id);
            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                Items.Add(new OrderItemResponse
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = 1,
                    UnitPrice = product.Price
                });
            }
            NotifyDataChanged();
        }
        public void RemoveFromBasket(int productId)
        {
            var itemToRemove = Items.FirstOrDefault(x => x.ProductId == productId);
            if (itemToRemove != null)
            {
                if(itemToRemove.Quantity > 1)
                {
                    itemToRemove.Quantity--;
                }
                else
                {
                    Items.Remove(itemToRemove);
                }
                 NotifyDataChanged();
            }
        }
        public async Task LoadActiveOrder(int tableId)
        {
            try
            {
                var response = await _http.GetFromJsonAsync<OrderDetailResponse>($"api/Orders/GetActiveOrderDetails/{tableId}");
                if (response != null && response.Items != null)
                {
                    OrderedItems = response.Items; // Birbaşa Response siyahısını mənimsədirik
                    NotifyDataChanged();
                }
            }
            catch 
            { 
                NotifyDataChanged();
            }
        }
        public async Task<bool> SendOrderToBackend(int tableId)
        {
            try
            {
                var orderRequest = new CreateOrderRequest
                {
                    TableId = tableId,
                    Items = Items.Select(x => new OrderItemRequest
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.UnitPrice
                    }).ToList()
                };
                var response = await _http.PostAsJsonAsync("api/Orders/CreateOrder", orderRequest);

                if (response.IsSuccessStatusCode)
                {
                    await LoadActiveOrder(tableId);
                    Items.Clear();
                    await orderService.LoadExistingOrderAsync(tableId);
                    NotifyDataChanged();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public decimal GetBasketTotal() => Items.Sum(x => x.UnitPrice * x.Quantity);
        public decimal GetOrderedTotal() => OrderedItems.Sum(x => x.UnitPrice * x.Quantity);

        private void NotifyDataChanged() => OnChange?.Invoke();
    }
}