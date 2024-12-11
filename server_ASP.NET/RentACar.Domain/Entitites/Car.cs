using RentACar.Domain.Entitites.Identity;
using RentACar.Domain.Entitites.Enum;
using RentACar.Domain.Entitites.Enum.Car;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain.Entitites
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(8)]
        public string RegNumber { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Model { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Make { get; set; } = null!;

        [Required]
        public int MakeYear { get; set; }

        public bool AirCondition { get; set; }
        public int Seats { get; set; }

        public int Doors { get; set; }

        public bool NavigationSystem { get; set; }

        public Fuel Fuel { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public string ImageUrl { get; set; } = null!;

        public Transmission Transmission { get; set; }

        [Required]
        public decimal DailyRate { get; set; }

        [Required]
        public bool IsAvailable { get; set; } = true;

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public int DealerId { get; set; }

        public Dealer Dealer { get; set; } = null!;

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
