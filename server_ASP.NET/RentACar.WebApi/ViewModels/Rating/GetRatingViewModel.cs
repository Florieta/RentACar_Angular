namespace RentACar.WebApi.ViewModels.Rating
{
    /// <summary>
    /// View Model for a ratig / DTO
    /// </summary>
    public class GetRatingViewModel
    {
        public int Id { get; set; }
        public int Rate { get; set; }

        public int CarId { get; set; }

        public int RenterId { get; set; }
    }
}
