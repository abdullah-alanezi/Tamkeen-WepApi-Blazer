using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
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
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // تسجيل المستودع العام (اختياري)
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // تسجيل المستودعات الخاصة - كرر هذا لكل جدول لديك
            services.AddScoped<ITrainingProgramRepository, TrainingProgramRepository>();
            services.AddScoped<ITraineeRepository, TraineeRepository>();
            services.AddScoped<IInterviewRepository, InterviewRepository>();
            services.AddScoped<ITrainingApplicationRepository, TrainingApplicationRepository>();
            services.AddScoped<IMonthlyEvaluationRepository, MonthlyEvaluationRepository>();

            return services;
        }
    }
}