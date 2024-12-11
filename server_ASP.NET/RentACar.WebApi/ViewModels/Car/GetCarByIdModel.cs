namespace RentACar.WebApi.ViewModels.Car
{
    public class GetCarByIdModel
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

        public string ImageUrl { get; set; } = null!;

        public string Transmission { get; set; } = null!;

        public decimal DailyRate { get; set; }

        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; } 

    }
}
