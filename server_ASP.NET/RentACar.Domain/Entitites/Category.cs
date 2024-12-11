using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain.Entitites
{
    public class Category
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public IEnumerable<Car> Cars { get; set; } = new List<Car>();
    }
}
