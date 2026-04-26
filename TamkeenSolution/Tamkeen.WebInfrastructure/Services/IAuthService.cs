using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.WebInfrastructure.Models;

namespace Tamkeen.WebInfrastructure.Services
{
    public interface IAuthService
    {
        Task<string?> GetAccessToken();
        Task<AuthResponse?> GetTokens();
        Task SaveTokens(AuthResponse tokens);
        Task Logout();
    }
}
