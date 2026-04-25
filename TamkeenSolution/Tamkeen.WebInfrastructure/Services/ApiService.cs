using System.Net.Http.Json;
using System.Text.Json;
using Tamkeen.Core.Common;
using Tamkeen.WebInfrastructure.Enums;
using Tamkeen.WebInfrastructure.Services;

namespace Tamkeen.WebInfrastructure.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;
        private readonly RequestManager _requestManager;
        private readonly LoadingService _loader; // إضافة المحمل

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ApiService(HttpClient http, RequestManager requestManager, LoadingService loader)
        {
            _http = http;
            _requestManager = requestManager;
            _loader = loader;
        }

        public async Task<Result<TResponse>> SendAsync<TRequest, TResponse>(
            MyHttpMethod method,
            string endpoint,
            TRequest? body = default)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                return Result<TResponse>.Failure("Endpoint is empty");

            var requestKey = $"{method}:{endpoint}";
            var ct = _requestManager.Register(requestKey);

            try
            {
                HttpResponseMessage response = method switch
                {
                    MyHttpMethod.GET => await _http.GetAsync(endpoint, ct),
                    MyHttpMethod.POST => await _http.PostAsJsonAsync(endpoint, body, ct),
                    MyHttpMethod.PUT => await _http.PutAsJsonAsync(endpoint, body, ct),
                    MyHttpMethod.DELETE => await _http.DeleteAsync(endpoint, ct),
                    _ => throw new ArgumentOutOfRangeException()
                };

                return await HandleResponse<TResponse>(response, ct);
            }
            catch (Exception ex)
            {
                return Result<TResponse>.Failure($"Error: {ex.Message}");
            }
            finally
            {
                _requestManager.Unregister(requestKey);
            }
        }

        private async Task<Result<TResponse>> HandleResponse<TResponse>(
            HttpResponseMessage response,
            CancellationToken ct)
        {
            if (!response.IsSuccessStatusCode)
            {
                // محاولة قراءة رسالة الخطأ من السيرفر إذا كانت بصيغة JSON أو نص
                var errorContent = await response.Content.ReadAsStringAsync(ct);
                return Result<TResponse>.Failure(string.IsNullOrWhiteSpace(errorContent)
                    ? $"Error: {response.StatusCode}"
                    : errorContent);
            }

            try
            {
                var data = await response.Content.ReadFromJsonAsync<TResponse>(_jsonOptions, ct);

                if (data is null)
                    return Result<TResponse>.Failure("Empty response");

                return Result<TResponse>.Success(data);
            }
            catch (Exception ex)
            {
                return Result<TResponse>.Failure($"Parse error: {ex.Message}");
            }
        }
    }
}