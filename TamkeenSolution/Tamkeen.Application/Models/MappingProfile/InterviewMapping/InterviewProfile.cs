using AutoMapper;
using Tamkeen.Core.Models.DTOs;
using Tamkeen.Domain.Entities.Interview;

namespace Tamkeen.Application.Models.MappingProfile.InterviewMapping
{
    public class InterviewProfile : Profile
    {
        public InterviewProfile()
        {
            CreateMap<Interview, InterviewDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap();
        }
    }
}