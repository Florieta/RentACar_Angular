using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain.Entitites
{
    public class Location
    {
        public int Id { get; set; }
        public string LocationName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public bool IsDeleted { get; set; }

        [InverseProperty("PickUpLocation")]
        public ICollection<Order> PickUpLocations { get; set; } = new List<Order>();

        [InverseProperty("DropOffLocation")]
        public ICollection<Order> DropOffLocations { get; set; } = new List<Order>();
    }
}
