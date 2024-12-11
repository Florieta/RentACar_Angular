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
    /// Get ratings by car handler
    /// </summary>
    public class GetRatingsByCarIdHandler : IRequestHandler<GetRatingsByCarId, List<Rating>>
    {
        private readonly IUnitOfWork unitOfWorkRepo;

        public GetRatingsByCarIdHandler(IUnitOfWork unitOfWorkRepo)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
        }
        /// <summary>
        ///Gets a list of ratings by identifier
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns a list od ratings by car </returns>
        public async Task<List<Rating>> Handle(GetRatingsByCarId request, CancellationToken cancellationToken)
        {
            return await this.unitOfWorkRepo.RatingRepository.GetAllAsyncByCarId(request.CarId);
        }
    }
}
