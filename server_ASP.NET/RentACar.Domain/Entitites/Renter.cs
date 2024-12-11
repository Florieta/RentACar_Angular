using RentACar.Domain.Entitites.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain.Entitites
{
    public class Renter
    {
        public int Id { get; set; }
        public int Age { get; set; }

        public string DrivingLicenceNumber { get; set; } = null!;

        public DateTime ExpiredDate { get; set; }

        public string Address { get; set; } = null!;

        public string ApplicationUserId { get; set; } = null!;
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Rating> Reatings { get; set; } = new List<Rating>();
    }
}
