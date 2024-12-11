using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Abstract
{
    /// <summary>
    /// <see cref="Order"/> repository 
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Gets a <see cref="Order"/> asynchronously by identifier.
        /// </summary>
        /// <param name="orderId">Getting by identifier.</param>
        /// <returns>Returns <see cref="Order"/> object.</returns>
        
        Task<Order> GetByIdAsync(int id);

        /// <summary>
        /// Gets all <see cref="Order"/>s asynchronously by identifier.
        /// </summary>
        /// <returns>Returns a list of <see cref="Order"/>s.</returns>
        /// <param name="renterId">Filtering the orders by renterId.</param>
        Task<List<Order>> GetAllOrdersByRenterIdAsync(int renterId);

        /// <summary>
        /// Creates a <see cref="Order"/> asynchronously.
        /// </summary>
        /// <param name="order">Creating a order.</param>
        Task AddAsync(Order order);

        /// <summary>
        /// Soft delete of a <see cref="Order"/>.
        /// </summary>
        /// <param name="order">Marks the order as deleted.</param>
        Task Remove(Order order);

        /// <summary>
        /// Gets all <see cref="Order"/>s asynchronously.
        /// </summary>
        /// <returns>Returns a list of <see cref="Order"/>s.</returns>
        Task<List<Order>> GetAllAsync();
    }
}
