using System.ComponentModel.DataAnnotations;

namespace RentACar.WebApi.ViewModels.Location
{
    public class EditLocationViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string LocationName { get; set; } = null!;
        [StringLength(50, MinimumLength = 5)]
        public string Address { get; set; } = null!;
    }
}
