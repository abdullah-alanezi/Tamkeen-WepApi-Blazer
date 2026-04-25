using AutoMapper;
using Tamkeen.Core.Models.TrainingProgram.Request;
using Tamkeen.Core.Models.TrainingProgram.Response;
using Tamkeen.Domain.Entities.Trainee;
using Tamkeen.Domain.Enums;

namespace Tamkeen.Application.Models.MappingProfile.ProgramMapping
{
    public class TrainingProgramProfile : Profile
    {
        public TrainingProgramProfile()
        {
            // =========================
            // ENTITY -> RESPONSE
            // =========================
            CreateMap<TrainingProgram, TrainingProgramResponse>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status))

                .ForMember(dest => dest.ApplicationsCount,
                    opt => opt.MapFrom(src => src.Applications.Count));

            // =========================
            // REQUEST -> ENTITY
            // =========================
            CreateMap<TrainingProgramCreateDto, TrainingProgram>();

            // ❌ لا ReverseMap
        }
    }
}