using AutoMapper;
using RentACar.Application.Cars.Commands.Create;
using RentACar.Application.Cars.Commands.Update;
using RentACar.Domain.Entitites;
using RentACar.Domain.Models;
using RentACar.WebApi.ViewModels.Car;

namespace RentACar.WebApi.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, GetCarViewModel>()
                .ForMember("CategoryName", x => x.MapFrom(y => y.Category.CategoryName));
            CreateMap<Car, GetCarByIdModel>();
            CreateMap<Car, EditCarViewModel>();
            CreateMap<AddCarModel, CreateCar>();
            CreateMap<EditCarViewModel, UpdateCar>();
        }
    }
}
