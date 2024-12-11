using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RentACar.Application.Abstract
{
    /// <summary>
    /// <see cref="Car"/> repository 
    /// </summary>
    public interface ICarRepository
    {
        /// <summary>
        /// Gets a <see cref="Car"/> asynchronously by identifier.
        /// </summary>
        /// <param name="carId">Getting by identifier.</param>
        /// <returns>Returns <see cref="Car"/> object.</returns>
        Task<Car> GetByIdAsync(int carId);

        /// <summary>
        /// Creates a <see cref="Car"/> asynchronously.
        /// </summary>
        /// <param name="car">Creating a car.</param>
        Task AddAsync(Car car);

        /// <summary>
        /// Soft delete of a <see cref="Car"/>.
        /// </summary>
        /// <param name="car">Marks the car as deleted.</param>
        void Remove(Car car);

        /// <summary>
        /// Gets all <see cref="Car"/>s asynchronously.
        /// </summary>
        /// <returns>Returns a list of <see cref="Car"/>s.</returns>
        Task<List<Car>> GetAllAsync();

        /// <summary>
        /// Gets the latest 6 <see cref="Car"/>s asynchronously.
        /// </summary>
        /// <returns>Returns a list of 6 or less<see cref="Car"/>s.</returns>
        Task<List<Car>> GetLatestCarsAsync();
        /// <summary>
        /// Updates a <see cref="Car"/> asynchronously.
        /// </summary>
        /// <param name="car">The updated <see cref="Car"/> object.</param>
        /// <returns>Returns the updated <see cref="Car"/>.</returns>
        Task Update(Car car);

        /// <summary>
        /// Gets all <see cref="Car"/>s asynchronously by identifier.
        /// </summary>
        /// <returns>Returns a list of <see cref="Car"/>s.</returns>
        /// <param name="dealerId">Filtering the cars by delaerId.</param>

        Task<List<Car>> GetCarsByDealerIdAsync(int dealerId);

        /// <summary>
        /// Gets a <see cref="Car"/> asynchronously by identifier.
        /// </summary>
        /// <param name="carId">Getting by identifier.</param>
        /// <returns>Returns <see cref="Car"/> object.</returns>
        Task<Car> GetByIdAsyncWithTracking(int carId);

    }
}
