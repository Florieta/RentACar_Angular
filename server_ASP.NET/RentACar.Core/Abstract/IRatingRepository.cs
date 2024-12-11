using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Abstract
{
    /// <summary>
    /// <see cref="Rating"/> repository 
    /// </summary>
    public interface IRatingRepository
    {

        /// <summary>
        /// Gets a <see cref="Rating"/> asynchronously by identifier.
        /// </summary>
        /// <param name="ratingId">Getting by identifier.</param>
        /// <returns>Returns <see cref="Rating"/> object.</returns>
        Task<Rating> GetByIdAsync(int ratingId);

        /// <summary>
        /// Gets all <see cref="Rating"/>s asynchronously by identifier.
        /// </summary>
        /// <returns>Returns a list of <see cref="Rating"/>s.</returns>
        /// <param name="carId">Filtering the orders by carId.</param>
        Task<List<Rating>> GetAllAsyncByCarId(int carId);

        /// <summary>
        /// Creates a <see cref="Rating"/> asynchronously.
        /// </summary>
        /// <param name="rating">Creating a rating.</param>
        Task AddAsync(Rating rating);

        /// <summary>
        /// Gets all <see cref="Rating"/>s asynchronously.
        /// </summary>
        /// <returns>Returns a list of <see cref="Rating"/>s.</returns>
        Task<List<Rating>> GetAllAsync();

        /// <summary>
        /// Gets all <see cref="Rating"/>s asynchronously by two identifier.
        /// </summary>
        /// <returns>Returns a list of <see cref="Rating"/>s.</returns>
        /// <param name="renterId">Filtering the ratings by renterId.</param>
        /// <param name="carId">Filtering the ratings by carId.</param>
        
        Task<Rating> GetByCarIdAndRenterIdAsync(int carId, int renterId);

    }
}
