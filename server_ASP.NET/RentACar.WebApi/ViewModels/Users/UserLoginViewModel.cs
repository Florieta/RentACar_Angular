using System.ComponentModel.DataAnnotations;

namespace RentACar.WebApi.ViewModels.Users
{
    public class UserLoginViewModel
    {

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
