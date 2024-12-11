using MediatR;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Cars.Queries
{
    public class GetAllCarsByDealerId : IRequest<List<Car>>
    {
        public int DealerId { get; set; }
    }
}
