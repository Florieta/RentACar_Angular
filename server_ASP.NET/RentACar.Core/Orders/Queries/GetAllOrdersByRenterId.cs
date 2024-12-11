using MediatR;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Orders.Queries
{
    public class GetAllCarsByDealerId : IRequest<List<Order>>
    {
        public int RenterId { get; set; }
    }
}
