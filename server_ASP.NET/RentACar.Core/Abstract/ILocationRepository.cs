using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Abstract
{
    /// <summary>
    /// <see cref="Location"/> repository 
    /// </summary>
    public interface ILocationRepository
    {

        /// <summary>
        /// Gets a <see cref="Location"/> asynchronously by identifier.
        /// </summary>
        /// <param name="locationId">Getting by identifier.</param>
        /// <returns>Returns <see cref="Location"/> object.</returns>
        Task<Location> GetByIdAsync(int locationId);

        /// <summary>
        /// Creates a <see cref="Location"/> asynchronously.
        /// </summary>
        /// <param name="location">Creating a location.</param>
        Task AddAsync(Location location);

        /// <summary>
        /// Gets all <see cref="Location"/>s asynchronously.
        /// </summary>
        /// <returns>Returns a list of <see cref="Location"/>s.</returns>
        Task<List<Location>> GetAllAsync();

    }
}
