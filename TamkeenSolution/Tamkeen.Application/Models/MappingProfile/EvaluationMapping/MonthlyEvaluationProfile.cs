using AutoMapper;
using Tamkeen.Core.Models.DTOs;
using Tamkeen.Domain.Entities.Evaluations;

namespace Tamkeen.Application.Models.MappingProfile.EvaluationMapping
{
    public class MonthlyEvaluationProfile : Profile
    {
        public MonthlyEvaluationProfile()
        {
            CreateMap<MonthlyEvaluation, MonthlyEvaluationDto>()
                // حساب الدرجة النهائية تلقائياً أثناء النقل
                .ForMember(dest => dest.TotalGrade, opt => opt.MapFrom(src => (src.AttendanceGrade + src.PerformanceGrade) / 2))
                .ReverseMap();
        }
    }
}