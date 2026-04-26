using Microsoft.AspNetCore.Components;
using MudBlazor;
using Tamkeen.Core.Common;
using Tamkeen.Core.Models.Trainee.Request;
using Tamkeen.Core.Models.Trainee.Response;
using Tamkeen.WebInfrastructure.Constants.Routes;
using Tamkeen.WebInfrastructure.Http;
using Tamkeen.WebInfrastructure.Services;

namespace WebClient.Pages.Trainees
{
    public partial class Trainees : IDisposable
    {
        [Inject] private IDialogService DialogService { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private LoadingService Loader { get; set; } = null!;
        [Inject] private HTTPClientInterceptor Interceptor { get; set; } = null!;

        private string _searchString = "";
        private List<TraineeResponse> _trainees = new();
        private string? _errorMessage;

        protected override async Task OnInitializedAsync() => await LoadTrainees();

        private async Task LoadTrainees()
        {
            try
            {
                Loader.Show();
                _errorMessage = null;

                // 1. اطلب الاستجابة بنوع Result بدلاً من List مباشرة
                var result = await Interceptor.EnqueueRequestAsync<Result<List<TraineeResponse>>>(
                    ApiEndpoints.Trainees.GetAll,
                    HttpMethod.Get,
                    requireAuth: false);

                // 2. تحقق من نجاح العملية واستخرج البيانات (Data)
                if (result != null && result.IsSuccess)
                {
                    _trainees = result.Data ?? new(); // استخراج القائمة من حقل Data
                    if (_trainees.Count == 0) _errorMessage = "لا توجد بيانات حالياً.";
                }
                else
                {
                    _errorMessage = result?.ErrorMessage ?? "حدث خطأ أثناء جلب البيانات.";
                }
            }
            catch (Exception ex)
            {
                _errorMessage = "خطأ في الاتصال بالسيرفر: " + ex.Message;
            }
            finally
            {
                Loader.Hide();
            }
        }

        private bool FilterFunc(TraineeResponse element)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            return element.FullName.Contains(_searchString, StringComparison.OrdinalIgnoreCase) ||
                   element.Email.Contains(_searchString, StringComparison.OrdinalIgnoreCase) ||
                   element.Major.Contains(_searchString, StringComparison.OrdinalIgnoreCase);
        }

        private string GetInitials(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "TR";
            var words = name.Trim().Split(' ');
            return words.Length > 1
                ? $"{words[0][0]}{words[words.Length - 1][0]}".ToUpper()
                : $"{words[0][0]}".ToUpper();
        }

        // تم التحديث لاستخدام TraineeCreateForm
        private async Task OpenAddDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small, FullWidth = true };
            var dialog = await DialogService.ShowAsync<TraineeCreateForm>("إضافة متدرب جديد", options);
            var result = await dialog.Result;

            if (!result.Canceled && result.Data is TraineeCreateDto model)
                await CreateTrainee(model);
        }

        // تم التحديث لاستخدام TraineeEditForm
        private async Task OpenEditDialog(TraineeResponse trainee)
        {
            var model = new TraineeCreateDto
            {
                Id = trainee.Id,
                Email = trainee.Email,
                PhoneNumber = trainee.PhoneNumber,
                University = trainee.University,
                Major = trainee.Major,
                // محاولة استخراج الاسم الأول والأخير من FullName للعرض في النموذج
                FirstName = trainee.FullName.Split(' ')[0],
                LastName = trainee.FullName.Contains(" ") ? trainee.FullName.Substring(trainee.FullName.IndexOf(" ") + 1) : ""
            };

            var parameters = new DialogParameters { ["Model"] = model };
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            var dialog = await DialogService.ShowAsync<TraineeEditForm>("تعديل بيانات المتدرب", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled && result.Data is TraineeCreateDto updatedModel)
                await UpdateTrainee(trainee.Id, updatedModel);
        }

        private async Task CreateTrainee(TraineeCreateDto model)
        {
            try
            {
                Loader.Show();
                var result = await Interceptor.EnqueueRequestAsync<Result<TraineeResponse>>(
                    ApiEndpoints.Trainees.Create, HttpMethod.Post, model, false);

                if (result != null)
                {
                    Snackbar.Add("تمت إضافة المتدرب بنجاح", Severity.Success);
                    await LoadTrainees();
                }
            }
            catch (Exception ex) { Snackbar.Add(ex.Message, Severity.Error); }
            finally { Loader.Hide(); }
        }

        private async Task UpdateTrainee(Guid id, TraineeCreateDto model)
        {
            try
            {
                Loader.Show();
                model.Id = id;
                var result = await Interceptor.EnqueueRequestAsync<TraineeResponse>(
                    ApiEndpoints.Trainees.Update, HttpMethod.Put, model, true);

                if (result != null)
                {
                    Snackbar.Add("تم تحديث البيانات بنجاح", Severity.Success);
                    await LoadTrainees();
                }
            }
            catch (Exception ex) { Snackbar.Add(ex.Message, Severity.Error); }
            finally { Loader.Hide(); }
        }

        public void Dispose() { }
    }
}