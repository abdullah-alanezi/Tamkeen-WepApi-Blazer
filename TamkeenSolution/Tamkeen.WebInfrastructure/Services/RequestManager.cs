using System.Collections.Concurrent;

namespace Tamkeen.WebInfrastructure.Services
{
    public class RequestManager
    {
        private readonly ConcurrentDictionary<string, CancellationTokenSource> _requests = new();

        public CancellationToken Register(string key)
        {
            if (_requests.TryGetValue(key, out var oldCts))
            {
                try
                {
                    oldCts.Cancel();
                    oldCts.Dispose();
                }
                catch { }
            }

            var cts = new CancellationTokenSource();
            _requests[key] = cts;

            return cts.Token;
        }

        public void Unregister(string key)
        {
            if (_requests.TryRemove(key, out var cts))
            {
                try
                {
                    cts.Cancel();
                    cts.Dispose();
                }
                catch { }
            }
        }

        public void CancelAll()
        {
            foreach (var kvp in _requests)
            {
                try
                {
                    kvp.Value.Cancel();
                    kvp.Value.Dispose();
                }
                catch { }
            }

            _requests.Clear();
        }
    }
}