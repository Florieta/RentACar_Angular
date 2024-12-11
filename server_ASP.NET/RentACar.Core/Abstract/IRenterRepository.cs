using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Abstract
{
    /// <summary>
    /// <see cref="Renter"/> repository 
    /// </summary>
    public interface IRenterRepository
    {
        /// <summary>
        /// Gets a <see cref="Renter"/> asynchronously by identifier.
        /// </summary>
        /// <param name="renterId">Getting by identifier.</param>
        /// <returns>Returns <see cref="Renter"/> object.</returns>
        Task<Renter> GetByIdAsync(int renterId);

        /// <summary>
        /// Creates a <see cref="Renter"/> asynchronously.
        /// </summary>
        /// <param name="renter">Creating a renter.</param>
        Task AddAsync(Renter renter);

        /// <summary>
        /// Gets all <see cref="Renter"/>s asynchronously.
        /// </summary>
        /// <returns>Returns a list of <see cref="Renter"/>s.</returns>
        Task<List<Renter>> GetAllAsync();

        /// <summary>
        /// Updates a <see cref="Renter"/> asynchronously.
        /// </summary>
        /// <param name="renter">The updated <see cref="Renter"/> object.</param>
        /// <returns>Returns the updated <see cref="Renter"/>.</returns>
        Task Update(Renter renter);
    }
}
