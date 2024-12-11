using RentACar.Domain.Entitites.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain.Entitites
{
    public class Dealer
    {
        public int Id { get; set; }

        public string CompanyName { get; set; } = null!;

        public string CompanyNumber { get; set; } = null!;
        public string Address { get; set; } = null!;

        public string ApplicationUserId { get; set; } = null!;

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
