using MediatR;
using RentACar.Application.Abstract;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Cars.Commands.Update
{
    /// <summary>
    /// Update car handler
    /// </summary>
    public class UpdateCarHandler : IRequestHandler<UpdateCar, Car>
    {
        private readonly IUnitOfWork uniteOfWorkRepo;

        public UpdateCarHandler(IUnitOfWork uniteOfWorkRepo)
        {
            this.uniteOfWorkRepo = uniteOfWorkRepo;
        }
        /// <summary>
        /// Updates the car
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns the updated car</returns>
        public async Task<Car> Handle(UpdateCar request, CancellationToken cancellationToken)
        {
           var oldCar = await uniteOfWorkRepo.CarRepository.GetByIdAsync(request.Id);
            var car = new Car
            {
                Id = request.Id,
                RegNumber = request.RegNumber,
                Make = request.Make,
                Model = request.Model,
                MakeYear = request.MakeYear,
                AirCondition = request.AirCondition,
                Seats = request.Seats,
                Doors = request.Doors,
                ImageUrl = oldCar.ImageUrl,
                NavigationSystem = request.NavigationSystem,
                Fuel = request.Fuel,
                Transmission = request.Transmission,
                DailyRate = request.DailyRate,
                CategoryId = request.CategoryId,
                DealerId = request.DealerId
            };
            


            await this.uniteOfWorkRepo.CarRepository.Update(car);
            await this.uniteOfWorkRepo.SaveAsync();

            return car;
        }
    }
}
