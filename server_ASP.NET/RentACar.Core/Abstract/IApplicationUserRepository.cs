using RentACar.Domain.Entitites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Abstract
{
    /// <summary>
    /// <see cref="ApplicationUser"/> repository 
    /// </summary>
    public interface IApplicationUserRepository
    {
        /// <summary>
        /// Gets a <see cref="ApplicationUser"/> asynchronously by identifier.
        /// </summary>
        /// <param name="applicationUserId">Getting by identifier.</param>
        /// <returns>Returns <see cref="ApplicationUser"/> object.</returns>
        Task<ApplicationUser> GetByIdAsync(string applicationUserId);

        /// <summary>
        /// Updates a specific <see cref="ApplicationUser"/>.
        /// </summary>
        /// <param name="user">A specific user.</param>
        /// <returns>Returns the updated <see cref="ApplicationUser"/> object.</returns>
        Task Update(ApplicationUser user);
    }
}
