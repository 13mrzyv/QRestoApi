namespace QResto.Blazor.Services
{
    public class UserProvider
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public bool IsAdmin => Role == "Admin"; // Köməkçi qısa yol
    }
}
