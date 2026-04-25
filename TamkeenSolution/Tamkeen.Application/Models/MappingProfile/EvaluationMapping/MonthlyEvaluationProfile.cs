using AutoMapper;

using Tamkeen.Core.Models.MonthlyEvaluation.Request;
using Tamkeen.Core.Models.MonthlyEvaluation.Response;
using Tamkeen.Domain.Entities.Evaluations;

namespace Tamkeen.Application.Models.MappingProfile.EvaluationMapping
{
    public class MonthlyEvaluationProfile : Profile
    {
        public MonthlyEvaluationProfile()
        {
            // =========================
            // ENTITY -> RESPONSE
            // =========================
            CreateMap<MonthlyEvaluation, MonthlyEvaluationResponse>()
                .ForMember(dest => dest.TraineeName,
                    opt => opt.MapFrom(src =>
                        src.TrainingApplication.Trainee.FirstName + " " +
                        src.TrainingApplication.Trainee.LastName))

                .ForMember(dest => dest.ProgramTitle,
                    opt => opt.MapFrom(src =>
                        src.TrainingApplication.TrainingProgram.Title))

                .ForMember(dest => dest.PerformanceGrade,
                    opt => opt.MapFrom(src =>
                        (src.AttendanceGrade + src.PerformanceGrade) / 2.0));

            // =========================
            // REQUEST -> ENTITY
            // =========================
            CreateMap<MonthlyEvaluationCreateDto, MonthlyEvaluation>();

            // ❌ لا ReverseMap
        }
    }
}