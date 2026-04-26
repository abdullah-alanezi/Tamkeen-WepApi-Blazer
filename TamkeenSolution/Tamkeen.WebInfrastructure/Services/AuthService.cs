using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.WebInfrastructure.Models;

namespace Tamkeen.WebInfrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILocalStorageService _localStorage;
        private const string TokenKey = "authToken";
        private const string RefreshTokenKey = "refreshToken";

        public AuthService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string?> GetAccessToken()
        {
            //var tokens = await GetTokens();
            //return tokens?.AccessToken;
            return null;
        }

        public async Task<AuthResponse?> GetTokens()
        {
            return await _localStorage.GetItemAsync<AuthResponse>(TokenKey);
        }

        public async Task SaveTokens(AuthResponse tokens)
        {
            await _localStorage.SetItemAsync(TokenKey, tokens);
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(TokenKey);
        }
    }
}
