using AutoMapper;
using TPFinal_GSC.Dto;
using TPFinal_GSC.Entities;
using TPFinal_GSC.Models;

namespace TPFinal_GSC.Map
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Thing, ThingViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Loan, LoanForCreationDto>().ReverseMap();
            CreateMap<Loan, LoanDto>().ReverseMap();
        }
    }
}
