using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Domain.Entitites;
using RentACar.Domain.Entitites.Enum.Car;

namespace RentACar.Application.Ratings.Commands.Create
{
    public class CreateRating : IRequest<Rating>
    {
        public int Rate { get; set; }

        public int CarId { get; set; }
       
        public int RenterId { get; set; }
        
    }
}
