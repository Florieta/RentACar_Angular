using MediatR;
using RentACar.Application.Abstract;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Orders.Queries
{
    /// <summary>
    /// Get all orders by renterId handler
    /// </summary>
    public class GetAllCarsByDealerIdHandler : IRequestHandler<GetAllCarsByDealerId, List<Order>>
    {
        private readonly IUnitOfWork unitOfWorkRepo;

        public GetAllCarsByDealerIdHandler(IUnitOfWork unitOfWorkRepo)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
        }
        /// <summary>
        /// Gets all orders by identifier
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns a list of orders by renterId</returns>
        public async Task<List<Order>> Handle(GetAllCarsByDealerId request, CancellationToken cancellationToken)
        {
            var result =  await this.unitOfWorkRepo.OrderRepository.GetAllOrdersByRenterIdAsync(request.RenterId);

            foreach (var order in result)
            {
                if(order.PickUpDateAndTime < DateTime.UtcNow)
                {
                    order.IsActive = false;
                }
            }

            return result;
        }
    }
}
