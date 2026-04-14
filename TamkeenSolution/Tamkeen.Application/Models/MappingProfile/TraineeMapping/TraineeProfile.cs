using AutoMapper;
using Tamkeen.Core.Models.DTOs;
using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Application.Models.MappingProfile.TraineeMapping
{
    public class TraineeProfile : Profile
    {
        public TraineeProfile()
        {
            CreateMap<Trainee, TraineeDto>()
                // إذا أردت دمج الاسم الأول والأخير تلقائياً في الـ DTO
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ReverseMap();
        }
    }
}