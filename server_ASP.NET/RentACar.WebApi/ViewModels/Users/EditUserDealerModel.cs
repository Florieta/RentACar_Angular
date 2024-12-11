namespace RentACar.WebApi.ViewModels.Users
{
    public class EditUserDealerModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int DealerId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string CompanyNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
