using MediatR;
using RentACar.Application.Abstract;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Ratings.Queries
{
    /// <summary>
    /// Get rating by car and renter handler
    /// </summary>
    public class GetRatingByCarAndByRenterHandler : IRequestHandler<GetRatingByCarAndByRenter, Rating>
    {
        private readonly IUnitOfWork unitOfWorkRepo;

        public GetRatingByCarAndByRenterHandler(IUnitOfWork unitOfWorkRepo)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
        }

        /// <summary>
        /// Gets a rating by carId and by renters
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns a rating</returns>
        public async Task<Rating> Handle(GetRatingByCarAndByRenter request, CancellationToken cancellationToken)
        {
            return await this.unitOfWorkRepo.RatingRepository.GetByCarIdAndRenterIdAsync(request.CarId, request.RenterId);
        }
    }
}
