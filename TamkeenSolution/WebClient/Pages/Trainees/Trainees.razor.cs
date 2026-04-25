using Microsoft.AspNetCore.Components;
using MudBlazor;
using Tamkeen.Core.Common;
using Tamkeen.Core.Models.Trainee.Request;
using Tamkeen.Core.Models.Trainee.Response;
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
        private List<TraineeResponse> _trainees = new();
        private string? _errorMessage;

        // =========================
        // INIT
        // =========================
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

                var apiResult = await Api.SendRequestAsync<object, Result<List<TraineeResponse>>>(
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
                _errorMessage = ex.Message;
            }
            finally
            {
                Loader.Hide();
            }
        }

        // =========================
        // CREATE
        // =========================
        private async Task OpenAddDialog()
        {
            var options = new DialogOptions
            {
                CloseButton = true,
                FullWidth = true,
                MaxWidth = MaxWidth.Small
            };

            var dialog = await DialogService.ShowAsync<TraineeCreateForm>(
                "إضافة متدرب",
                options);

            var result = await dialog.Result;

            if (!result.Canceled && result.Data is TraineeCreateDto model)
            {
                await CreateTrainee(model);
            }
        }

        private async Task CreateTrainee(TraineeCreateDto model)
        {
            try
            {
                Loader.Show();

                var apiResult = await Api.SendRequestAsync<TraineeCreateDto, Result<TraineeResponse>>(
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
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                Loader.Hide();
            }
        }

        // =========================
        // EDIT
        // =========================
        private async Task OpenEditDialog(TraineeResponse trainee)
        {
            var parameters = new DialogParameters
            {
                ["Model"] = new TraineeCreateDto
                {
                    FirstName = trainee.FullName.Split(' ')[0],
                    LastName = trainee.FullName.Contains(" ")
                        ? trainee.FullName.Split(' ')[1]
                        : "",
                    Email = trainee.Email,
                    PhoneNumber = trainee.PhoneNumber,
                    University = trainee.University,
                    Major = trainee.Major,
                    ResumeUrl = trainee.ResumeUrl,
                    UserSSN = "" // إذا ما يرجع من Response
                }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                FullWidth = true,
                MaxWidth = MaxWidth.Small
            };

            var dialog = await DialogService.ShowAsync<TraineeEditForm>(
                "تعديل المتدرب",
                parameters,
                options);

            var result = await dialog.Result;

            if (!result.Canceled && result.Data is TraineeCreateDto model)
            {
                await UpdateTrainee(trainee.Id, model);
            }
        }

        private async Task UpdateTrainee(Guid id, TraineeCreateDto model)
        {
            try
            {
                Loader.Show();

                var request = new
                {
                    Id = id,
                    model.FirstName,
                    model.LastName,
                    model.Email,
                    model.PhoneNumber,
                    model.University,
                    model.Major,
                    model.ResumeUrl,
                    model.UserSSN
                };

                var apiResult = await Api.SendRequestAsync<object, Result<TraineeResponse>>(
                    MyHttpMethod.PUT,
                    ApiEndpoints.Trainees.Update,
                    request);

                if (apiResult.IsSuccess && apiResult.Data?.IsSuccess == true)
                {
                    Snackbar.Add("تم التحديث بنجاح", Severity.Success);
                    await LoadTrainees();
                }
                else
                {
                    Snackbar.Add(apiResult.Data?.ErrorMessage ?? "فشل التحديث", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                Loader.Hide();
            }
        }

        // =========================
        // FILTER
        // =========================
        private bool FilterFunc(TraineeResponse e)
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

        private string GetInitials(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return "?";

            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
                return parts[0][0].ToString().ToUpper();

            return $"{parts[0][0]}{parts[1][0]}".ToUpper();
        }
    }
}