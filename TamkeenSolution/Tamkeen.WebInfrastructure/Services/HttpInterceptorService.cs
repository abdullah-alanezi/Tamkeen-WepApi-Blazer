using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Tamkeen.Core.Common;
using Tamkeen.WebInfrastructure.Enums;
using Toolbelt.Blazor;

using MyHttpMethod = Tamkeen.WebInfrastructure.Enums.MyHttpMethod;
namespace Tamkeen.WebInfrastructure.Services
{
    public class HttpInterceptorService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClientInterceptor _interceptor;
        private readonly NavigationManager _navManager;
        private readonly LoadingService _loader;
        public HttpInterceptorService(
            HttpClient httpClient,
            HttpClientInterceptor interceptor,
            NavigationManager navManager,
            LoadingService loader)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
            _navManager = navManager ?? throw new ArgumentNullException(nameof(navManager));

            RegisterEvent();
            _loader = loader;
        }

        public void RegisterEvent()
        {
            if (_interceptor != null)
                _interceptor.AfterSend += HandleResponseGlobal;
        }

        public void DisposeEvent()
        {
            if (_interceptor != null)
                _interceptor.AfterSend -= HandleResponseGlobal;
        }

        private void HandleResponseGlobal(object? sender, HttpClientInterceptorEventArgs e)
        {
            if (e?.Response != null && !e.Response.IsSuccessStatusCode)
            {
                if (e.Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    _navManager?.NavigateTo("/login");
            }
        }

        // استخدام الـ Enum الخاص بك (Tamkeen.WebInfrastructure.Enums.HttpMethod)
        public async Task<Result<TResponse>> SendRequestAsync<TRequest, TResponse>(
                    MyHttpMethod method,
                    string endpoint,
                    TRequest? body = default)
        {
            try
            {
                // 3. إظهار اللودر فور بدء الطلب
                _loader.Show();

                Console.WriteLine($"=== Sending Request: {endpoint} ===");

                if (string.IsNullOrWhiteSpace(endpoint))
                    return Result<TResponse>.Failure("Endpoint cannot be null or empty");

                HttpResponseMessage response;

                switch (method)
                {
                    case MyHttpMethod.GET:
                        response = await _httpClient.GetAsync(endpoint);
                        break;
                    case MyHttpMethod.POST:
                        response = await _httpClient.PostAsJsonAsync(endpoint, body);
                        break;
                    case MyHttpMethod.PUT:
                        response = await _httpClient.PutAsJsonAsync(endpoint, body);
                        break;
                    case MyHttpMethod.DELETE:
                        response = await _httpClient.DeleteAsync(endpoint);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(method), method, null);
                }

                return await ProcessResponse<TResponse>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return Result<TResponse>.Failure($"Connection error: {ex.Message}");
            }
            finally
            {
                // 4. إخفاء اللودر دائماً عند انتهاء الطلب (نجاح أو فشل)
                _loader.Hide();
            }
        }

        private async Task<Result<TResponse>> ProcessResponse<TResponse>(HttpResponseMessage response)
        {
            if (response == null)
                return Result<TResponse>.Failure("Response is null");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    // ✅ هذا سيتعامل مع Result<T> أو أي نوع آخر
                    var data = await response.Content.ReadFromJsonAsync<TResponse>();
                    return Result<TResponse>.Success(data!);
                }
                catch (Exception ex)
                {
                    return Result<TResponse>.Failure($"JSON parsing error: {ex.Message}");
                }
            }

            var error = await response.Content.ReadAsStringAsync();
            return Result<TResponse>.Failure(!string.IsNullOrEmpty(error) ? error : response.ReasonPhrase ?? "Unknown error");
        }
    }
}