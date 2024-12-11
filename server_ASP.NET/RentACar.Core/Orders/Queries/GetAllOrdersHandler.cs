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
    /// Get all orders handler
    /// </summary>
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrders, List<Order>>
    {
        private readonly IUnitOfWork unitOfWorkRepo;

        public GetAllOrdersHandler(IUnitOfWork unitOfWorkRepo)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
        }
        /// <summary>
        /// Gets all orders 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns a list of orders</returns>
        public async Task<List<Order>> Handle(GetAllOrders request, CancellationToken cancellationToken)
        {
            return await this.unitOfWorkRepo.OrderRepository.GetAllAsync();
        }
    }
}
