using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Domain.Entitites;
using MediatR;
using RentACar.Application.Abstract;

namespace RentACar.Application.Ratings.Commands.Create
{
    /// <summary>
    /// Create rating handler
    /// </summary>
    public class CreateRatingHandler : IRequestHandler<CreateRating, Rating>
    {
        private readonly IUnitOfWork unitOfWorkRepo;

        public CreateRatingHandler(IUnitOfWork unitOfWorkRepo)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
        }
        /// <summary>
        /// Creates a rating
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns the given rating</returns>
        public async Task<Rating> Handle(CreateRating request, CancellationToken cancellationToken)
        {
            var rating = new Rating
            {
                Rate = request.Rate,
                CarId = request.CarId,
                RenterId = request.RenterId,
                
            };

            await this.unitOfWorkRepo.RatingRepository.AddAsync(rating);
            await this.unitOfWorkRepo.SaveAsync();

            return rating;
        }
    }
}
