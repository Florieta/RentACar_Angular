namespace RentACar.WebApi.ViewModels.Location
{
    public class GetLocationViewModel
    {
        public int Id { get; set; }
        public string LocationName { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
