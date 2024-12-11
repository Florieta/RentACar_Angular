using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain.Entitites
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        [Range(1, 10)]
        public int Rate { get; set; }

        public int CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = null!;

        public int RenterId { get; set; }
        [ForeignKey(nameof(RenterId))]
        public Renter Renter { get; set; } = null!;
    }
}
