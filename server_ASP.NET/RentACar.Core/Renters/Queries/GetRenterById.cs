using MediatR;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Renters.Queries
{
    public class GetRenterById : IRequest<Renter>
    {
        public int Id { get; set; }
}
}
