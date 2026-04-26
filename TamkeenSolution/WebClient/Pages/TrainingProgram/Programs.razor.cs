using Microsoft.AspNetCore.Components;
using MudBlazor;
using Tamkeen.Core.Common;
using Tamkeen.Core.Models.TrainingProgram.Response;
using Tamkeen.WebInfrastructure.Constants.Routes;
using Tamkeen.WebInfrastructure.Http;
using Tamkeen.WebInfrastructure.Services;

namespace WebClient.Pages.TrainingProgram
{
    public partial class Programs: IDisposable
    {
        [Inject] private IDialogService DialogService { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private LoadingService Loader { get; set; } = null!;
        [Inject] private HTTPClientInterceptor Interceptor { get; set; } = null!;

        private List<TrainingProgramResponse> _programs = new List<TrainingProgramResponse>();

        protected override async Task OnInitializedAsync()
        {
            await LoadPrograms();
        }

        private async Task LoadPrograms()
        {
            Loader.Show();
            var result = await Interceptor.EnqueueRequestAsync<Result<List<TrainingProgramResponse>>>(ApiEndpoints.Programs.GetAll, HttpMethod.Get,requireAuth: false);

            if (result != null && result.IsSuccess)
            {
                _programs = result.Data ?? new(); // استخراج القائمة من حقل Data
                //if (_programs.Count == 0) _errorMessage = "لا توجد بيانات حالياً.";
            }

            Loader.Hide();
        }

        private async Task AddProgram()
        {
            Snackbar.Add("فتح نافذة إضافة برنامج", Severity.Info);

            // افتح Dialog هنا لاحقاً
        }

        private async Task EditProgram(TrainingProgramResponse program)
        {
            Snackbar.Add($"تعديل: {program.Title}", Severity.Warning);

            // افتح Dialog للتعديل
        }

        private async Task DeleteProgram(TrainingProgramResponse program)
        {
            bool? result = await DialogService.ShowMessageBoxAsync(
                "تأكيد الحذف",
                $"هل أنت متأكد من حذف البرنامج ({program.Title})؟",
                yesText: "حذف",
                cancelText: "إلغاء"
            );

            if (result == true)
            {
                Snackbar.Add("تم الحذف (مؤقت)", Severity.Success);

                // هنا تضيف API Delete
            }
        }
        public void Dispose()
        {
            
        }
    }
}
