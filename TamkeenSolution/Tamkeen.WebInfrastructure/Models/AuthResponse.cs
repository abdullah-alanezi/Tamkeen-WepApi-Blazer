using System;
using System.Collections.Generic;
using System.Text;

namespace Tamkeen.WebInfrastructure.Models
{
    public class AuthResponse
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
