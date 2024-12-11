using AutoMapper;
using RentACar.Domain.Entitites;
using RentACar.WebApi.ViewModels.Dealer;

namespace RentACar.WebApi.Profiles
{
    public class DealerProfile : Profile
    {
        public DealerProfile()
        {
            CreateMap<Dealer, GetDealerViewModel>()
                .ForMember("FirstName", x => x.MapFrom(y => y.ApplicationUser.FirstName))
                .ForMember("LastName", x => x.MapFrom(y => y.ApplicationUser.LastName))
                .ForMember("UserName", x => x.MapFrom(y => y.ApplicationUser.UserName))
                .ForMember("Email", x => x.MapFrom(y => y.ApplicationUser.Email))
                .ForMember("PhoneNumber", x => x.MapFrom(y => y.ApplicationUser.PhoneNumber));
        }
    }
}
