using AutoMapper;
using Tamkeen.Core.Models.Trainee.Request;
using Tamkeen.Core.Models.Trainee.Response;
using Tamkeen.Core.Models.TrainingApplication.Request;
using Tamkeen.Core.Models.TrainingApplication.Response;
using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Application.Models.MappingProfile.ApplicationMapping
{
    public class TrainingApplicationProfile : Profile
    {
        public TrainingApplicationProfile()
        {
            // =========================
            // ENTITY -> RESPONSE
            // =========================
            CreateMap<TrainingApplication, TrainingApplicationResponse>()
                .ForMember(dest => dest.TraineeName,
                    opt => opt.MapFrom(src =>
                        src.Trainee != null
                            ? src.Trainee.FirstName + " " + src.Trainee.LastName
                            : string.Empty))

                .ForMember(dest => dest.ProgramTitle,
                    opt => opt.MapFrom(src =>
                        src.TrainingProgram != null
                            ? src.TrainingProgram.Title
                            : string.Empty))

                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status));

            // =========================
            // REQUEST -> ENTITY (CREATE)
            // =========================
            CreateMap<TrainingApplicationCreateDto, TrainingApplication>();

            // ❌ لا ReverseMap هنا
        }
    }
}