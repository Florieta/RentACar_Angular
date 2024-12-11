using MediatR;
using RentACar.Application.Abstract;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Dealers.Queries
{
    /// <summary>
    /// Get dealer by id handler
    /// </summary>
    public class GetDealerByIdAsyncHandler : IRequestHandler<GetDealerByIdAsync, Dealer>
    {
        private readonly IUnitOfWork unitOfWorkRepo;

        public GetDealerByIdAsyncHandler(IUnitOfWork unitOfWorkRepo)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
        }
        /// <summary>
        /// Get a dealer by identifier
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns a specific renter</returns>
        public async Task<Dealer> Handle(GetDealerByIdAsync request, CancellationToken cancellationToken)
        {
            return await this.unitOfWorkRepo.DealerRepository.GetByIdAsync(request.Id);
        }
    }
}
