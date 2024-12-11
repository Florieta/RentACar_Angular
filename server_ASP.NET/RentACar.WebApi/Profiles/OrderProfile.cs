using AutoMapper;
using RentACar.Application.Orders.Commands.Create;
using RentACar.Domain.Entitites;
using RentACar.WebApi.ViewModels.Order;

namespace RentACar.WebApi.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, GetOrderViewModel>()
                .ForMember("CarMake", x => x.MapFrom(y => y.Car.Make))
                .ForMember("CarModel", x => x.MapFrom(y => y.Car.Model))
                .ForMember("RegNumber", x => x.MapFrom(y => y.Car.RegNumber))
                .ForMember("PickUpLocation", x => x.MapFrom(y => y.PickUpLocation.LocationName))
                .ForMember("DropOffLocation", x => x.MapFrom(y => y.DropOffLocation.LocationName));
            CreateMap<AddOrderModel, CreateOrder>();
        }
    }
}
