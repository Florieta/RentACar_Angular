using MediatR;
using RentACar.Application.Abstract;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Locations.Queries
{
    /// <summary>
    /// Get all categories
    /// </summary>
    public class GetAllLocationsHandler : IRequestHandler<GetAllLocations, List<Location>>
    {
        private readonly IUnitOfWork unitOfWorkRepo;

        public GetAllLocationsHandler(IUnitOfWork unitOfWorkRepo)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
        }
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns a list of categories</returns>
        public async Task<List<Location>> Handle(GetAllLocations request, CancellationToken cancellationToken)
        {
            return await this.unitOfWorkRepo.LocationRepository.GetAllAsync();
        }
    }
}
