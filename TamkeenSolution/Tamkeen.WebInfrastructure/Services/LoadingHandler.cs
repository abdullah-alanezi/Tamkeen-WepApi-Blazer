using System;
using System.Collections.Generic;
using System.Text;

namespace Tamkeen.WebInfrastructure.Services
{
    public class LoadingHandler : DelegatingHandler
    {
        private readonly LoadingService _loadingService;
        public LoadingHandler(LoadingService loadingService) => _loadingService = loadingService;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _loadingService.Show();
            try { return await base.SendAsync(request, cancellationToken); }
            finally { _loadingService.Hide(); }
        }
    }
}
