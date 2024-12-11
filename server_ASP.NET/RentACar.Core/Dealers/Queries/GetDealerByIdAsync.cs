using MediatR;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Dealers.Queries
{
    public class GetDealerByIdAsync : IRequest<Dealer>
    {
        public int Id { get; set; }
    }
}
