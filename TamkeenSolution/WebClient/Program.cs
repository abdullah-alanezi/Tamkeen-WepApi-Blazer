using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Tamkeen.WebInfrastructure.Services;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using WebClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClientInterceptor();

// ✅ تأكد أن هذا البورت يطابق تشغيل API (من الـ log: https://localhost:7297)
builder.Services.AddScoped(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7297/") };
    httpClient.EnableIntercept(sp);
    return httpClient;
});

builder.Services.AddScoped<HttpInterceptorService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();