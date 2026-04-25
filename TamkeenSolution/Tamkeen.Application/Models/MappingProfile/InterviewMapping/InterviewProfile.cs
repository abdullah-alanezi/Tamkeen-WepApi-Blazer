using AutoMapper;
using Tamkeen.Core.Models.Interview.Request;
using Tamkeen.Core.Models.Interview.Response;
using Tamkeen.Domain.Entities.Interview;
using Tamkeen.Domain.Enums;

namespace Tamkeen.Application.Models.MappingProfile.InterviewMapping
{
    public class InterviewProfile : Profile
    {
        public InterviewProfile()
        {
            // =========================
            // ENTITY -> RESPONSE
            // =========================
            CreateMap<Interview, InterviewResponse>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status))

                .ForMember(dest => dest.TraineeName,
                    opt => opt.MapFrom(src =>
                        src.TrainingApplication.Trainee.FirstName + " " +
                        src.TrainingApplication.Trainee.LastName))

                .ForMember(dest => dest.ProgramTitle,
                    opt => opt.MapFrom(src =>
                        src.TrainingApplication.TrainingProgram.Title));

            // =========================
            // REQUEST -> ENTITY
            // =========================
            CreateMap<InterviewCreateDto, Interview>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src =>
                        Enum.Parse<InterviewStatus>(src.Status)));

            // ❌ لا ReverseMap
        }
    }
}