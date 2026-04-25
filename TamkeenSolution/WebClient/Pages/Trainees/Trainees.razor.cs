using Microsoft.AspNetCore.Components;
using MudBlazor;
using Tamkeen.Core.Common;
using Tamkeen.Core.Models.DTOs;
using Tamkeen.WebInfrastructure.Constants;
using Tamkeen.WebInfrastructure.Services;
using Tamkeen.WebInfrastructure.Enums;

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

        // =========================
        // LOAD
        // =========================
        private async Task LoadTrainees()
        {
            try
            {
                Loader.Show();
                _errorMessage = null;

                var apiResult = await Api.SendRequestAsync<object, Result<List<TraineeDto>>>(
                    MyHttpMethod.GET,
                    ApiEndpoints.Trainees.GetAll);

                if (apiResult.IsSuccess && apiResult.Data?.IsSuccess == true)
                {
                    _trainees = apiResult.Data.Data ?? new();
                }
                else
                {
                    _errorMessage = apiResult.Data?.ErrorMessage
                                    ?? apiResult.ErrorMessage
                                    ?? "فشل تحميل البيانات";
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

        // =========================
        // ADD
        // =========================
        private async Task OpenAddDialog()
        {
            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            var dialog = await DialogService.ShowAsync<TraineeCreateForm>(
                "إضافة متدرب",
                options
            );

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
                Loader.Show();

                var apiResult = await Api.SendRequestAsync<TraineeDto, Result<TraineeDto>>(
                    MyHttpMethod.POST,
                    ApiEndpoints.Trainees.Create,
                    model);

                if (apiResult.IsSuccess && apiResult.Data?.IsSuccess == true)
                {
                    Snackbar.Add("تمت إضافة المتدرب بنجاح", Severity.Success);
                    await LoadTrainees();
                }
                else
                {
                    Snackbar.Add(apiResult.Data?.ErrorMessage ?? "فشل الإضافة", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"خطأ: {ex.Message}", Severity.Error);
            }
            finally
            {
                Loader.Hide();
            }
        }

        // =========================
        // EDIT
        // =========================
        private async Task OpenEditDialog(TraineeDto trainee)
        {
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
                }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            var dialog = await DialogService.ShowAsync<TraineeEditForm>(
                "تعديل بيانات المتدرب",
                parameters,
                options
            );

            var result = await dialog.Result;

            if (!result.Canceled && result.Data is TraineeDto updatedModel)
            {
                await UpdateTrainee(updatedModel);
            }
        }

        private async Task UpdateTrainee(TraineeDto model)
        {
            try
            {
                Loader.Show();

                var apiResult = await Api.SendRequestAsync<TraineeDto, Result<bool>>(
                    MyHttpMethod.PUT,
                    ApiEndpoints.Trainees.Update,
                    model);

                if (apiResult.IsSuccess && apiResult.Data?.IsSuccess == true)
                {
                    Snackbar.Add("تم تحديث البيانات بنجاح", Severity.Success);
                    await LoadTrainees();
                }
                else
                {
                    Snackbar.Add(apiResult.Data?.ErrorMessage ?? "فشل التحديث", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"خطأ: {ex.Message}", Severity.Error);
            }
            finally
            {
                Loader.Hide();
            }
        }

        // =========================
        // FILTER
        // =========================
        private bool FilterFunc(TraineeDto e)
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            return e.FullName.Contains(_searchString, StringComparison.OrdinalIgnoreCase)
                || e.Email.Contains(_searchString, StringComparison.OrdinalIgnoreCase)
                || e.Major.Contains(_searchString, StringComparison.OrdinalIgnoreCase);
        }

        // =========================
        // DISPOSE
        // =========================
        public void Dispose()
        {
            Api?.DisposeEvent();
        }
    }
}