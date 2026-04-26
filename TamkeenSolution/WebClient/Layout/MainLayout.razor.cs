using Microsoft.AspNetCore.Components;
using Tamkeen.WebInfrastructure.Services;
using Tamkeen.WebInfrastructure.Http; // التوجيه الجديد للـ Interceptor

namespace WebClient.Layout
{
    public partial class MainLayout : LayoutComponentBase, IDisposable
    {
        // نستخدم الـ Interceptor الجديد (إذا كنت تريد مراقبة شيء ما، وإلا يمكن حذفه من هنا تماماً)
        [Inject] private HTTPClientInterceptor Interceptor { get; set; } = null!;
        [Inject] private LoadingService LoaderService { get; set; } = null!;

        protected bool _drawerOpen = true;
        protected bool _isLoading;

        protected void DrawerToggle() => _drawerOpen = !_drawerOpen;

        protected override void OnInitialized()
        {
            // ✅ لاحظ: حذفنا Interceptor.RegisterEvent() لأن النظام الجديد يعمل تلقائياً عبر الـ Channel

            // الربط مع خدمة التحميل (اللودر) يبقى كما هو لأنه يراقب حالة الـ Requests
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
            // ✅ حذفنا Interceptor.DisposeEvent() 
            // التنظيف يقتصر على أحداث اللودر فقط
            LoaderService.OnShow -= HandleShowLoader;
            LoaderService.OnHide -= HandleHideLoader;
        }
    }
}