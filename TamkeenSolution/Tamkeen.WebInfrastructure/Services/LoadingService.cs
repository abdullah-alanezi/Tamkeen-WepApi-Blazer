using System;
using System.Collections.Generic;
using System.Text;

namespace Tamkeen.WebInfrastructure.Services
{

    public class LoadingService
    {
        private int _loadingCount = 0;
        public event Action OnShow;
        public event Action OnHide;

        public void Show()
        {
            _loadingCount++;
            if (_loadingCount == 1) OnShow?.Invoke();
        }

        public void Hide()
        {
            _loadingCount--;
            if (_loadingCount <= 0)
            {
                _loadingCount = 0;
                OnHide?.Invoke();
            }
        }
    }
}
