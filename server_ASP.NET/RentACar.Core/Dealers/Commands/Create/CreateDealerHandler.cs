using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Domain.Entitites;
using MediatR;
using RentACar.Application.Abstract;

namespace RentACar.Application.Dealers.Commands.Create
{
    /// <summary>
    /// Create delaer handler
    /// </summary>
    public class CreateDealerHandler : IRequestHandler<CreateDealer, Dealer>
    {
        private readonly IUnitOfWork unitOfWorkRepo;

        public CreateDealerHandler(IUnitOfWork unitOfWorkRepo)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
        }
        /// <summary>
        /// Creates a new delaer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns the new created dealer</returns>
        public async Task<Dealer> Handle(CreateDealer request, CancellationToken cancellationToken)
        {
            var dealer = new Dealer
            {
                Id = request.Id,
               CompanyNumber = request.CompanyNumber,
               CompanyName = request.CompanyName
            };

            await this.unitOfWorkRepo.DealerRepository.AddAsync(dealer);
            await this.unitOfWorkRepo.SaveAsync();

            return dealer;
        }
    }
}
