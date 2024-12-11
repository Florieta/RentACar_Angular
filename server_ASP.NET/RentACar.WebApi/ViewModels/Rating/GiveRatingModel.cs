namespace RentACar.WebApi.ViewModels.Rating
{
    /// <summary>
    /// View model for goving a rating / DTO
    /// </summary>
    public class GiveRatingModel
    {
        public int Rate { get; set; }
        public int CarId { get; set; }
        public int RenterId { get; set; }
    }
}
