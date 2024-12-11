using MediatR;
using RentACar.Application.Abstract;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Cars.Commands.Delete
{
    /// <summary>
    /// Delete car handler
    /// </summary>
    public class DeleteCarHandler : IRequestHandler<DeleteCar, Car>
    {
        private readonly IUnitOfWork uniteOfWorkRepo;

        public DeleteCarHandler(IUnitOfWork uniteOfWorkRepo)
        {
            this.uniteOfWorkRepo = uniteOfWorkRepo;
        }
        /// <summary>
        /// Marks the entity as deleted
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns the deleted car</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Car> Handle(DeleteCar request, CancellationToken cancellationToken)
        {
            var car = await this.uniteOfWorkRepo.CarRepository.GetByIdAsyncWithTracking(request.Id);

            if (car.IsDeleted)
            {
                throw new InvalidOperationException("This car is already deleted");
            }
           
            this.uniteOfWorkRepo.CarRepository.Remove(car);
            await this.uniteOfWorkRepo.SaveAsync();

            return car;
        }
    }
}
