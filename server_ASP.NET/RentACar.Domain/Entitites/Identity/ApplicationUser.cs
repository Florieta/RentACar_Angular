using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain.Entitites.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int? RenterId { get; set; }

        [ForeignKey(nameof(RenterId))]
        public Renter? Renter { get; set; }

        public int? DealerId { get; set; }

        [ForeignKey(nameof(DealerId))]
        public Dealer? Dealer { get; set; }
    }
}
