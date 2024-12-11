namespace RentACar.WebApi.ViewModels.Users
{
    public class EditUserRenterModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int RenterId { get; set; }
        public int Age { get; set; }
        public string DrivingLicenceNumber { get; set; } = null!;
        public DateTime ExpiredDate { get; set; }
        public string Address { get; set; } = null!;
    }
}
