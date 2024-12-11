using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Domain.Entitites;
using RentACar.Domain.Entitites.Enum.Car;
using RentACar.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace RentACar.Application.Cars.Commands.Create
{
    public class CreateCar : IRequest<Car>
    {
        public int Id { get; set; }
        public string RegNumber { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Make { get; set; } = null!;
        public int MakeYear { get; set; }
        public bool AirCondition { get; set; }
        public int Seats { get; set; }
        public int Doors { get; set; }
        public bool NavigationSystem { get; set; }
        public Fuel Fuel { get; set; }
        public string ImageUrl { get; set; } = null!;
        public Transmission Transmission { get; set; }
        public decimal DailyRate { get; set; }
        public int CategoryId { get; set; }
        public int DealerId { get; set; }
    }
}
