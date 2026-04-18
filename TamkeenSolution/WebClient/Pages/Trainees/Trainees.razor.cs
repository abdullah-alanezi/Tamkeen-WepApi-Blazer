using Microsoft.AspNetCore.Components;
using Tamkeen.Core.Common;  // ✅ استخدم الـ Result الموجود
using Tamkeen.Core.Models.DTOs;
using Tamkeen.WebInfrastructure.Constants;
using Tamkeen.WebInfrastructure.Services;

namespace WebClient.Pages.Trainees
{
    public partial class Trainees : IDisposable
    {
        [Inject] private HttpInterceptorService Api { get; set; } = null!;
        // أضف هذا المتغير في الكلاس
        private string _searchString = "";
        private List<TraineeDto> _trainees = new();
        private bool _loading = true;
        private string? _errorMessage;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _loading = true;
                _errorMessage = null;

                var endpoint = ApiEndpoints.Trainees.GetAll;

                // ✅ انتظر Result<T> من الـ API مباشرة
                var apiResult = await Api.SendRequestAsync<object, Result<List<TraineeDto>>>(
                    Tamkeen.WebInfrastructure.Enums.HttpMethod.GET,
                    endpoint);

                // ✅ apiResult هو Result<Result<List<TraineeDto>>>، لذا نستخدم apiResult.Data
                if (apiResult.IsSuccess && apiResult.Data != null && apiResult.Data.IsSuccess)
                {
                    _trainees = apiResult.Data.Data ?? new List<TraineeDto>();
                }
                else if (!apiResult.IsSuccess)
                {
                    _errorMessage = apiResult.ErrorMessage ?? "Failed to load trainees";
                }
                else if (apiResult.Data != null && !apiResult.Data.IsSuccess)
                {
                    _errorMessage = apiResult.Data.ErrorMessage ?? "Failed to load trainees from API";
                }
            }
            catch (Exception ex)
            {
                _errorMessage = $"Error: {ex.Message}";
            }
            finally
            {
                _loading = false;
            }
        }

        public void Dispose()
        {
            Api?.DisposeEvent();
        }

        private bool FilterFunc1(TraineeDto element) => FilterFunc(element, _searchString);

        private bool FilterFunc(TraineeDto element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.FullName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Major.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
    }
}