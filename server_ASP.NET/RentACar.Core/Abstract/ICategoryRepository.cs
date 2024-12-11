using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Abstract
{
    /// <summary>
    /// <see cref="Category"/> repository 
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Gets a <see cref="Category"/> asynchronously by identifier.
        /// </summary>
        /// <param name="categoryId">Getting by identifier.</param>
        /// <returns>Returns <see cref="Category"/> object.</return
        Task<Category> GetByIdAsync(int categoryId);

        /// <summary>
        /// Creates a <see cref="Category"/> asynchronously.
        /// </summary>
        /// <param name="category">Creating a category.</param>
        Task AddAsync(Category category);

        /// <summary>
        /// Gets all <see cref="Category"/>s asynchronously.
        /// </summary>
        /// <returns>Returns a list of <see cref="Category"/>s.</returns>
        Task<List<Category>> GetAllAsync();
    }
}
