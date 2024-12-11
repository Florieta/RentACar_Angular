using MediatR;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Ratings.Queries
{
    public class GetRatingByCarAndByRenter : IRequest<Rating>
    {
        public int CarId { get; set; }

        public int RenterId { get; set; }
    }
}
