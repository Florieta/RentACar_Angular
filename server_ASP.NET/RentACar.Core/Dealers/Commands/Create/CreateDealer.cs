using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Domain.Entitites;


namespace RentACar.Application.Dealers.Commands.Create
{
    public class CreateDealer : IRequest<Dealer>
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string CompanyNumber { get; set; } = null!;
        public string Address { get; set; } = null!;

    }
}
