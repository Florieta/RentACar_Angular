using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Domain.Entitites;

namespace RentACar.Application.Ratings.Queries
{
    public class GetRatingsByCarId : IRequest<List<Rating>>
    {
        public int CarId { get; set; }
    }
}
