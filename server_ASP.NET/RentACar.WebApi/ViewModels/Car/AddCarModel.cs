using RentACar.Domain.Entitites.Enum.Car;
using RentACar.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace RentACar.WebApi.ViewModels.Car
{
    public class AddCarModel
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

        public string Fuel { get; set; } = null!;

        public string Transmission { get; set; } = null!;

        public decimal DailyRate { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public int DealerId { get; set; }
    }
}
