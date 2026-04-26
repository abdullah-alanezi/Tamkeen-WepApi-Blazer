using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Blazored.LocalStorage; // <--- تأكد من وجود هذا السطر
using Tamkeen.WebInfrastructure.Constants.Http;
using Tamkeen.WebInfrastructure.Http;
using Tamkeen.WebInfrastructure.Services;
using WebClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// --- 1. تسجيل خدمات MudBlazor ---
builder.Services.AddMudServices();

// --- 2. تسجيل مكتبة LocalStorage (هذا هو السطر المفقود الذي يسبب الخطأ) ---
builder.Services.AddBlazoredLocalStorage();

// --- 3. إعداد الـ HttpClient الأساسي ---
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7297/")
});

// --- 4. تسجيل الخدمات (Infrastructure) ---

// تسجيل خدمة الهوية
builder.Services.AddScoped<IAuthService, AuthService>();

// تسجيل مدير تجديد التوكن
builder.Services.AddScoped<RefreshTokenManager>();

// تسجيل الـ Interceptor المركزي
builder.Services.AddScoped<HTTPClientInterceptor>();

// --- 5. تسجيل الخدمات المساعدة ---
builder.Services.AddScoped<LoadingService>();

// --- 6. تشغيل التطبيق ---
await builder.Build().RunAsync();