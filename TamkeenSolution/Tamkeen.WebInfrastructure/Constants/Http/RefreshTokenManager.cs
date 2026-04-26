using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using Tamkeen.WebInfrastructure.Constants.Routes;
using Tamkeen.WebInfrastructure.Models;
using Tamkeen.WebInfrastructure.Services;

namespace Tamkeen.WebInfrastructure.Constants.Http
{
    public class RefreshTokenManager
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public RefreshTokenManager(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<bool> RefreshAsync()
        {
            var tokens = await _authService.GetTokens();

            var response = await _httpClient.PostAsJsonAsync(
                ApiEndpoints.Identity.RefreshToken,
                tokens
            );

            if (!response.IsSuccessStatusCode)
                return false;

            var newTokens = await response.Content.ReadFromJsonAsync<AuthResponse>();

            await _authService.SaveTokens(newTokens);

            return true;
        }
    }
}
