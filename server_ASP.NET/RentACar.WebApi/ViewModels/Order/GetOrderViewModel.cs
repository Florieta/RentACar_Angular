using RentACar.Domain.Entitites.Enum.Order;

namespace RentACar.WebApi.ViewModels.Order
{
    public class GetOrderViewModel
    {
        public int Id { get; set; }
        public DateTime PickUpDateAndTime { get; set; }

        public DateTime DropOffDateAndTime { get; set; }

        public int Duration { get; set; }

        public decimal TotalAmount { get; set; }

        public string PaymentType { get; set; } = null!;

        public bool IsActive { get; set; } 

        public bool IsPaid { get; set; } 

        public string CarMake { get; set; } = null!;

        public string CarModel { get; set; } = null!;

        public string RegNumber { get; set; } = null!;

        public string PickUpLocation { get; set; } = null!;


        public string DropOffLocation { get; set; } = null!;

        public string? Insurance { get; set; }

        public string RenterId { get; set; } = null!;


    }
}
