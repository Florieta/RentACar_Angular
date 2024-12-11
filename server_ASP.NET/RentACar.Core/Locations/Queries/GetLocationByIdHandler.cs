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
    /// Get location by id handler
    /// </summary>
    public class GetLocationByIdHandler : IRequestHandler<GetLocationById, Location>
    {
        private readonly IUnitOfWork unitOfWorkRepo;

        public GetLocationByIdHandler(IUnitOfWork unitOfWorkRepo)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
        }
        /// <summary>
        /// Gets a location by identifier
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns a location</returns>
        public async Task<Location> Handle(GetLocationById request, CancellationToken cancellationToken)
        {
            return await this.unitOfWorkRepo.LocationRepository.GetByIdAsync(request.Id);
        }
    }
}
