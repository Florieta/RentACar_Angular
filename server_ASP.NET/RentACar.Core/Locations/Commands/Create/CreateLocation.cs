using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Domain.Entitites;
using RentACar.Domain.Entitites.Enum.Car;

namespace RentACar.Application.Locations.Commands.Create
{
    public class CreateLocation : IRequest<Location>
    {
        public string LocationName { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
