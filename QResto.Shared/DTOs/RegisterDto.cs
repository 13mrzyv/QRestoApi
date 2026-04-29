using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QResto.Shared.DTOs
{
    public class RegisterDto
    {
        // Ofisiantın tam adı (məsələn: Əli Məmmədov)
        public string FullName { get; set; }

        // Sənin dediyin o kiçik ad (məsələn: ofisiant1 və ya ali01)
        public string Username { get; set; }

        // Giriş üçün şifrə və ya PIN kod
        public string Password { get; set; }

        public string Role { get; set; } // "Admin" və ya "Waiter" və s.
    }
}
