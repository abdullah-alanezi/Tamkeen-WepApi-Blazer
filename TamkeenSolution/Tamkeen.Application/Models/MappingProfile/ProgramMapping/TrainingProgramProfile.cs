using AutoMapper;
using Tamkeen.Core.Models.DTOs;

using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Application.Models.MappingProfile.ProgramMapping
{
    public class TrainingProgramProfile : Profile
    {
        public TrainingProgramProfile()
        {
            CreateMap<TrainingProgram, TrainingProgramDto>()
                // تحويل الـ Enum إلى String لسهولة العرض في Blazor
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap();
        }
    }
}