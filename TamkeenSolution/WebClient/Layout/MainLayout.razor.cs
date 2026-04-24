using Microsoft.AspNetCore.Components;
using Tamkeen.WebInfrastructure.Services;

namespace WebClient.Layout
{
    public partial class MainLayout : LayoutComponentBase, IDisposable
    {
        [Inject] private HttpInterceptorService Interceptor { get; set; } = null!;
        [Inject] private LoadingService LoaderService { get; set; } = null!;

        protected bool _drawerOpen = true;
        protected bool _isLoading;

        protected void DrawerToggle() => _drawerOpen = !_drawerOpen;

        protected override void OnInitialized()
        {
            // تسجيل أحداث الـ Interceptor الخاصة بك
            Interceptor.RegisterEvent();

            // الربط مع خدمة التحميل (اللودر)
            LoaderService.OnShow += HandleShowLoader;
            LoaderService.OnHide += HandleHideLoader;
        }

        private void HandleShowLoader()
        {
            InvokeAsync(() =>
            {
                _isLoading = true;
                StateHasChanged();
            });
        }

        private void HandleHideLoader()
        {
            InvokeAsync(() =>
            {
                _isLoading = false;
                StateHasChanged();
            });
        }

        public void Dispose()
        {
            // تنظيف الأحداث عند الخروج
            Interceptor.DisposeEvent();
            LoaderService.OnShow -= HandleShowLoader;
            LoaderService.OnHide -= HandleHideLoader;
        }
    }
}