using AutoMapper;
using Tamkeen.Core.Models.DTOs;
using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Application.Models.MappingProfile.ApplicationMapping
{
    public class TrainingApplicationProfile : Profile
    {
        public TrainingApplicationProfile()
        {
            CreateMap<TrainingApplication, TrainingApplicationDto>()
                // جلب اسم المتدرب من علاقة الـ Navigation Property
                .ForMember(dest => dest.TraineeName, opt => opt.MapFrom(src => $"{src.Trainee.FirstName} {src.Trainee.LastName}"))
                // جلب عنوان البرنامج من علاقة الـ Navigation Property
                .ForMember(dest => dest.ProgramTitle, opt => opt.MapFrom(src => src.TrainingProgram.Title))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap();
        }
    }
}