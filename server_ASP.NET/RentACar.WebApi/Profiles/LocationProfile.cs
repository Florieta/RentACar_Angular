using AutoMapper;
using RentACar.Application.Locations.Commands.Create;
using RentACar.Domain.Entitites;
using RentACar.WebApi.ViewModels.Location;

namespace RentACar.WebApi.Profiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, GetLocationViewModel>();
            CreateMap<AddLocationModel, CreateLocation>();
        }
    }
}
