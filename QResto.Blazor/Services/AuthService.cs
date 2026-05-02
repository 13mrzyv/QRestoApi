using QResto.Shared.DTOs.Responses;
using System.Net.Http.Json;

namespace QResto.Blazor.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Login(string username, string password)
        {
            var loginDto = new { Username = username, Password = password };

            // API-dəki AuthController-in login endpoint-inə sorğu göndəririk
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                if (result != null)
                {
                    // HƏLƏLİK: Tokeni yaddaşa yazmaq və statusu dəyişmək hissəsini 
                    // növbəti addımda AuthenticationStateProvider ilə edəcəyik.
                    // İndi sadəcə yoxlamaq üçün True qaytarırıq.
                    return true;
                }
            }

            return false;
        }
    }
}
