using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QResto.Shared.DTOs.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public string Role { get; set; } // Bu sahəni əlavə edirik
        public string Username { get; set; }

    }
}
