using AutoMapper;
using Tamkeen.Core.Models.Trainee.Request;
using Tamkeen.Core.Models.Trainee.Response;
using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Application.Models.MappingProfile.TraineeMapping
{
    public class TraineeProfile : Profile
    {
        public TraineeProfile()
        {
            // =========================
            // ENTITY -> RESPONSE
            // =========================
            CreateMap<Trainee, TraineeResponse>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

            // =========================
            // REQUEST -> ENTITY
            // =========================
            CreateMap<TraineeCreateRequest, Trainee>();

            // ❌ لا ReverseMap
        }
    }
}