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
    /// Get rating handler
    /// </summary>
    public class GetRatingByIdHandler : IRequestHandler<GetRatingById, Rating>
    {
        private readonly IUnitOfWork unitOfWorkRepo;

        public GetRatingByIdHandler(IUnitOfWork unitOfWorkRepo)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
        }
        /// <summary>
        /// Gets a rating by identifier
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns a rating by id</returns>
        public async Task<Rating> Handle(GetRatingById request, CancellationToken cancellationToken)
        {
            return await this.unitOfWorkRepo.RatingRepository.GetByIdAsync(request.Id);
        }
    }
}
