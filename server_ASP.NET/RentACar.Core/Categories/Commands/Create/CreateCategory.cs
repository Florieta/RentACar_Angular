using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Domain.Entitites;

namespace RentACar.Application.Categories.Commands.Create
{
    public class CreateCategory : IRequest<Category>
    {
        public string CategoryName { get; set; } = null!;
    }
}
