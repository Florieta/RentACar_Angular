using MediatR;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Ratings.Queries
{
    public class GetRatingById : IRequest<Rating>
    {
        public int Id { get; set; }
    }
}
