using RentACar.Domain.Entitites.Enum.Order;
using RentACar.Domain.Entitites.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain.Entitites
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime PickUpDateAndTime { get; set; }

        public DateTime DropOffDateAndTime { get; set; }

        public int Duration { get; set; }

        public decimal TotalAmount { get; set; }

        public PaymentType PaymentType { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsPaid { get; set; } = false;

        public bool IsDeleted { get; set; } = false;

        public int CarId { get; set; }

        public Car Car { get; set; } = null!;

        public int PickUpLocationId { get; set; }

        public Location PickUpLocation { get; set; } = null!;

        public int DropOffLocationId { get; set; }

        public Location DropOffLocation { get; set; } = null!;

        public string? Insurance { get; set; }

        public int RenterId { get; set; }
        [ForeignKey(nameof(RenterId))]
        public Renter Renter { get; set; } = null!;
    }
}
