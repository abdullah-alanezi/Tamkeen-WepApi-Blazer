using Microsoft.AspNetCore.Components;
using MudBlazor;
using Tamkeen.Core.Common;
using Tamkeen.Core.Models.DTOs;
using Tamkeen.WebInfrastructure.Constants;
using Tamkeen.WebInfrastructure.Services;

namespace WebClient.Pages.Trainees
{
    public partial class Trainees : IDisposable
    {
        [Inject] private HttpInterceptorService Api { get; set; } = null!;
        [Inject] private IDialogService DialogService { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private LoadingService Loader { get; set; } = null!;

        private string _searchString = "";
        private List<TraineeDto> _trainees = new();
        
        private string? _errorMessage;

        protected override async Task OnInitializedAsync()
        {
            await LoadTrainees();
        }

        private async Task LoadTrainees()
        {
            try
            {
                Loader.Show();
               
                _errorMessage = null;

                var apiResult = await Api.SendRequestAsync<object, Result<List<TraineeDto>>>(
                    Tamkeen.WebInfrastructure.Enums.HttpMethod.GET,
                    ApiEndpoints.Trainees.GetAll);

                if (apiResult.IsSuccess && apiResult.Data != null && apiResult.Data.IsSuccess)
                {
                    _trainees = apiResult.Data.Data ?? new List<TraineeDto>();
                }
                else
                {
                    _errorMessage = apiResult.Data?.ErrorMessage ?? apiResult.ErrorMessage ?? "فشل تحميل البيانات";
                }
            }
            catch (Exception ex)
            {
                _errorMessage = $"خطأ: {ex.Message}";
            }
            finally
            {
                
                Loader.Hide();
            }
        }

        private async Task OpenEditDialog(TraineeDto trainee)
        {
            // نأخذ نسخة من البيانات لتجنب تعديل الجدول مباشرة قبل الضغط على "حفظ"
            var parameters = new DialogParameters
            {
                ["Model"] = new TraineeDto
                {
                    Id = trainee.Id,
                    FirstName = trainee.FirstName,
                    LastName = trainee.LastName,
                    Email = trainee.Email,
                    PhoneNumber = trainee.PhoneNumber,
                    University = trainee.University,
                    Major = trainee.Major
                    // أضف بقية الحقول هنا
                }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            var dialog = await DialogService.ShowAsync<TraineeEditForm>("تعديل بيانات المتدرب", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled && result.Data is TraineeDto updatedModel)
            {
                await UpdateTrainee(updatedModel);
            }
        }

        private async Task UpdateTrainee(TraineeDto model)
        {
            var apiResult = await Api.SendRequestAsync<TraineeDto, Result<bool>>(
                Tamkeen.WebInfrastructure.Enums.HttpMethod.PUT,
                ApiEndpoints.Trainees.Update,
                model);

            if (apiResult.IsSuccess && apiResult.Data != null && apiResult.Data.IsSuccess)
            {
                Snackbar.Add("تم تحديث البيانات بنجاح", Severity.Success);
                await LoadTrainees(); // تحديث القائمة
            }
            else
            {
                Snackbar.Add(apiResult.Data?.ErrorMessage ?? "حدث خطأ أثناء التحديث", Severity.Error);
            }
        }

        public void Dispose() => Api?.DisposeEvent();

        private bool FilterFunc1(TraineeDto element) => FilterFunc(element, _searchString);

        private bool FilterFunc(TraineeDto element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString)) return true;
            return element.FullName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                   element.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                   element.Major.Contains(searchString, StringComparison.OrdinalIgnoreCase);
        }


        private async Task OpenAddDialog()
        {
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
            var dialog = await DialogService.ShowAsync<TraineeCreateForm>("إضافة متدرب", options);
            var result = await dialog.Result;

            if (!result.Canceled && result.Data is TraineeDto newTrainee)
            {
                await CreateTrainee(newTrainee);
            }
        }

        private async Task CreateTrainee(TraineeDto model)
        {
            try
            {
                // إرسال الطلب للـ API باستخدام الـ Interceptor الذي أعددته
                var apiResult = await Api.SendRequestAsync<TraineeDto, Result<TraineeDto>>(
                    Tamkeen.WebInfrastructure.Enums.HttpMethod.POST,
                    ApiEndpoints.Trainees.Create,
                    model);

                if (apiResult.IsSuccess && apiResult.Data != null && apiResult.Data.IsSuccess)
                {
                    Snackbar.Add("تمت إضافة المتدرب بنجاح", Severity.Success);
                    await LoadTrainees(); // إعادة تحميل الجدول
                }
                else
                {
                    Snackbar.Add(apiResult.Data?.ErrorMessage ?? "فشل في عملية الإضافة", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"خطأ: {ex.Message}", Severity.Error);
            }
        }
    }


}