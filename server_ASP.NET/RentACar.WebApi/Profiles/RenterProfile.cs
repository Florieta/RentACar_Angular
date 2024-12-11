using AutoMapper;
using RentACar.Domain.Entitites;
using RentACar.WebApi.ViewModels.Renter;

namespace RentACar.WebApi.Profiles
{
    public class RenterProfile : Profile
    {
        public RenterProfile()
        {
            CreateMap<Renter, GetRenterViewModel>()
                .ForMember("FirstName", x => x.MapFrom(y => y.ApplicationUser.FirstName))
                .ForMember("LastName", x => x.MapFrom(y => y.ApplicationUser.LastName))
                .ForMember("UserName", x => x.MapFrom(y => y.ApplicationUser.UserName))
                .ForMember("Email", x => x.MapFrom(y => y.ApplicationUser.Email))
                .ForMember("PhoneNumber", x => x.MapFrom(y => y.ApplicationUser.PhoneNumber));
        }
    }
}
