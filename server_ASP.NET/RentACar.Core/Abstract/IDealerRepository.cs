using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Abstract
{
    /// <summary>
    /// <see cref="Dealer"/> repository 
    /// </summary>
    public interface IDealerRepository
    {
        /// <summary>
        /// Gets a <see cref="Dealer"/> asynchronously by identifier.
        /// </summary>
        /// <param name="dealerId">Getting by identifier.</param>
        /// <returns>Returns <see cref="Dealer"/> object.</returns>
        Task<Dealer> GetByIdAsync(int dealerId);

        /// <summary>
        /// Creates a <see cref="Dealer"/> asynchronously.
        /// </summary>
        /// <param name="dealer">Creating a dealer.</param>
        Task AddAsync(Dealer dealer);

        /// <summary>
        /// Gets all <see cref="Dealer"/>s asynchronously.
        /// </summary>
        /// <returns>Returns a list of <see cref="Dealer"/>s.</returns>
        Task<List<Dealer>> GetAllAsync();

        /// <summary>
        /// Updates a <see cref="Dealer"/> asynchronously.
        /// </summary>
        /// <param name="dealer">The updated <see cref="Dealer"/> object.</param>
        /// <returns>Returns the updated <see cref="Dealer"/>.</returns>
        Task Update(Dealer dealer);
    }
}
