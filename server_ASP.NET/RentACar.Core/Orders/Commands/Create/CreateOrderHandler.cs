using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Domain.Entitites;
using MediatR;
using RentACar.Application.Abstract;

namespace RentACar.Application.Orders.Commands.Create
{
    /// <summary>
    /// Create order handler
    /// </summary>
    public class CreateOrderHandler : IRequestHandler<CreateOrder, Order>
    {
        private readonly IUnitOfWork unitOfWorkRepo;

        public CreateOrderHandler(IUnitOfWork unitOfWorkRepo)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
        }
        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns the new created order</returns>
        public async Task<Order> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                PickUpDateAndTime = request.PickUpDateAndTime,
                DropOffDateAndTime = request.DropOffDateAndTime,
                Duration = request.Duration,
                TotalAmount = request.TotalAmount,
                PaymentType = request.PaymentType,
                IsPaid = request.IsPaid,
                CarId = request.CarId,
                PickUpLocationId = request.PickUpLocationId,
                DropOffLocationId = request.DropOffLocationId,
                Insurance = request.Insurance,
                RenterId = request.RenterId
            };

           
            await this.unitOfWorkRepo.OrderRepository.AddAsync(order);
            var car = await unitOfWorkRepo.CarRepository.GetByIdAsync(request.CarId);
            car.IsAvailable = false;
            await this.unitOfWorkRepo.SaveAsync();

            return order;
        }
    }
}
