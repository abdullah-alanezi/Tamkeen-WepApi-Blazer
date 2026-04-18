using Microsoft.AspNetCore.Components;

using Tamkeen.WebInfrastructure.Services;

namespace WebClient.Layout
{
    public partial class MainLayout : LayoutComponentBase, IDisposable
    {
        [Inject] private HttpInterceptorService Interceptor { get; set; } = null!;

        bool _drawerOpen = true;
        void DrawerToggle() => _drawerOpen = !_drawerOpen;

        protected override void OnInitialized()
        {
            Interceptor.RegisterEvent();
        }

        public void Dispose()
        {
            Interceptor.DisposeEvent();
        }
    }
}