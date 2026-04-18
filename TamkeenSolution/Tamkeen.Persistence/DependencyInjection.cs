using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tamkeen.Application.Interfaces.Evaluations;
using Tamkeen.Application.Interfaces.Generic;
using Tamkeen.Application.Interfaces.Interview;
using Tamkeen.Application.Interfaces.Trainee;
using Tamkeen.Persistence.Repositories.Evaluations;
using Tamkeen.Persistence.Repositories.Generic;
using Tamkeen.Persistence.Repositories.Interview;
using Tamkeen.Persistence.Repositories.Trainee;

namespace Tamkeen.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // التحقق من صحة المعاملات (اختياري ولكن يمنع الأخطاء)
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            // إضافة DbContext مع تحسين الأداء
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null);
                        sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    });

                // إضافة تحسينات الأداء (اختياري)
                options.EnableSensitiveDataLogging(false); // إيقاف في production
                options.EnableDetailedErrors(false);       // إيقاف في production
            });

            // تسجيل المستودع العام (Generic Repository)
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // تسجيل المستودعات الخاصة - كل مستودع في نطاق (Scope) جديد
            services.AddScoped<ITrainingProgramRepository, TrainingProgramRepository>();
            services.AddScoped<ITraineeRepository, TraineeRepository>();
            services.AddScoped<IInterviewRepository, InterviewRepository>();
            services.AddScoped<ITrainingApplicationRepository, TrainingApplicationRepository>();
            services.AddScoped<IMonthlyEvaluationRepository, MonthlyEvaluationRepository>();

            return services;
        }
    }
}