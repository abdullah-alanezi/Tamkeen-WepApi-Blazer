using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Tamkeen.WebInfrastructure.Models;
using Tamkeen.WebInfrastructure.Services;

namespace Tamkeen.WebInfrastructure.Http;

#region Request Job

public class HttpRequestJob<TResponse>
{
    public HttpMethod Method { get; init; }
    public string Url { get; init; }
    public object? Body { get; init; }
    public bool RequireAuth { get; init; }
    public CancellationToken CancellationToken { get; init; }
    public TaskCompletionSource<TResponse?> Tcs { get; } =
        new(TaskCreationOptions.RunContinuationsAsynchronously);
    public TimeSpan? Timeout { get; init; }
}

#endregion

#region Interceptor

public class HTTPClientInterceptor : IDisposable
{
    private readonly Channel<Func<Task>> _channel =
        Channel.CreateUnbounded<Func<Task>>(new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        });

    private readonly HttpClient _httpClient;
    private readonly IAuthService _authService;
    private readonly NavigationManager _navigation;
    private readonly ISnackbar _snackbar;

    private CancellationTokenSource _cts = new();

    public HTTPClientInterceptor(
        HttpClient httpClient,
        IAuthService authService,
        NavigationManager navigation,
        ISnackbar snackbar)
    {
        _httpClient = httpClient;
        _authService = authService;
        _navigation = navigation;
        _snackbar = snackbar;

        _ = Task.Run(ProcessQueueAsync);
    }

    #region Queue

    private async Task ProcessQueueAsync()
    {
        try
        {
            await foreach (var job in _channel.Reader.ReadAllAsync(_cts.Token))
            {
                try
                {
                    await job();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Interceptor Error: {ex}");
                }
            }
        }
        catch (OperationCanceledException) { }
    }

    #endregion

    #region Public API

    public Task<TResponse?> EnqueueRequestAsync<TResponse>(
        string url,
        HttpMethod method,
        object? body = null,
        bool requireAuth = false,
        CancellationToken cancellationToken = default)
    {
        var job = new HttpRequestJob<TResponse>
        {
            Url = url,
            Method = method,
            Body = body,
            RequireAuth = requireAuth,
            CancellationToken = cancellationToken
        };

        _ = _channel.Writer.WriteAsync(async () =>
            await ExecuteJobAsync(job));

        return job.Tcs.Task;
    }

    #endregion

    #region Core Logic

    private async Task ExecuteJobAsync<TResponse>(HttpRequestJob<TResponse> job)
    {
        try
        {
            var request = new HttpRequestMessage(job.Method, job.Url);

            request.Headers.Add("lang", "ar");

            // 🔐 Token
            if (job.RequireAuth)
            {
                var token = await _authService.GetAccessToken();

                if (string.IsNullOrEmpty(token))
                {
                    await Logout();
                    job.Tcs.TrySetResult(default);
                    return;
                }

                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            // 📦 Body
            if (job.Body != null)
            {
                var json = JsonSerializer.Serialize(job.Body);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            var response = await _httpClient.SendAsync(request);

            // ✅ Success
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TResponse>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                job.Tcs.TrySetResult(result);
                return;
            }

            // 🔁 Unauthorized → Refresh Token
            if (response.StatusCode == HttpStatusCode.Unauthorized && job.RequireAuth)
            {
                var refreshed = await TryRefreshToken();

                if (refreshed)
                {
                    var retry = await Retry<TResponse>(job);
                    job.Tcs.TrySetResult(retry);
                    return;
                }

                await Logout();
                job.Tcs.TrySetResult(default);
                return;
            }

            // ❌ Errors
            var error = await response.Content.ReadAsStringAsync();
            _snackbar.Add(error, Severity.Error);

            job.Tcs.TrySetResult(default);
        }
        catch (Exception ex)
        {
            job.Tcs.TrySetException(ex);
        }
    }

    private async Task<TResponse?> Retry<TResponse>(HttpRequestJob<TResponse> job)
    {
        var request = new HttpRequestMessage(job.Method, job.Url);

        var token = await _authService.GetAccessToken();

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        if (job.Body != null)
        {
            var json = JsonSerializer.Serialize(job.Body);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return default;

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TResponse>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    #endregion

    #region Token Refresh (مبسط)

    private async Task<bool> TryRefreshToken()
    {
        var tokens = await _authService.GetTokens();

        if (tokens == null)
            return false;

        var response = await _httpClient.PostAsync(
            "/api/auth/refresh",
            new StringContent(JsonSerializer.Serialize(tokens), Encoding.UTF8, "application/json"));

        if (!response.IsSuccessStatusCode)
            return false;

        var content = await response.Content.ReadAsStringAsync();

        var newTokens = JsonSerializer.Deserialize<AuthResponse>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (newTokens == null)
            return false;

        await _authService.SaveTokens(newTokens);

        return true;
    }

    #endregion

    #region Logout

    private async Task Logout()
    {
        await _authService.Logout();

        _navigation.NavigateTo("/login");

        _snackbar.Add("انتهت الجلسة، الرجاء تسجيل الدخول", Severity.Warning);
    }

    #endregion

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }
}

#endregion