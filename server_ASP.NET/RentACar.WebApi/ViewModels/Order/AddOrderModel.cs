using RentACar.Domain.Entitites.Enum.Order;

namespace RentACar.WebApi.ViewModels.Order
{
    public class AddOrderModel
    {
        public int Id { get; set; }
        public DateTime PickUpDateAndTime { get; set; }

        public DateTime DropOffDateAndTime { get; set; }

        public int Duration { get; set; }

        public decimal TotalAmount { get; set; }

        public string PaymentType { get; set; } = null!;

        public bool IsPaid { get; set; } = false;

        public int CarId { get; set; }

        public int PickUpLocationId { get; set; }

        public int DropOffLocationId { get; set; }

        public string? Insurance { get; set; }

        public int RenterId { get; set; }
    }
}
