using MediatR;
using RentACar.Application.Abstract;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Orders.Commands.Delete
{
    /// <summary>
    /// Delete order handler
    /// </summary>
    public class DeleteOrderHandler : IRequestHandler<DeleteOrder, Order>
    {
        private readonly IUnitOfWork uniteOfWorkRepo;

        public DeleteOrderHandler(IUnitOfWork uniteOfWorkRepo)
        {
            this.uniteOfWorkRepo = uniteOfWorkRepo;
        }
        /// <summary>
        /// Marks as deleted the order
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns the deleted order</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Order> Handle(DeleteOrder request, CancellationToken cancellationToken)
        {
            var order = await this.uniteOfWorkRepo.OrderRepository.GetByIdAsync(request.Id);
            var car = await this.uniteOfWorkRepo.CarRepository.GetByIdAsyncWithTracking(order.CarId);
            if (order.IsDeleted)
            {
                throw new InvalidOperationException("This order is already deleted");
            }
            car.IsAvailable = true;
            this.uniteOfWorkRepo.OrderRepository.Remove(order);
            await this.uniteOfWorkRepo.SaveAsync();

            return order;
        }
    }
}
