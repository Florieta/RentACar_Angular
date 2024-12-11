using MediatR;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Cars.Commands.Delete
{
    public class DeleteCar : IRequest<Car>
    {
        public int Id { get; set; }
    }
}
