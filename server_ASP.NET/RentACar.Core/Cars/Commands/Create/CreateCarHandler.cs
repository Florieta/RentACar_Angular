using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Domain.Entitites;
using MediatR;
using RentACar.Application.Abstract;
using Microsoft.Extensions.Configuration;

namespace RentACar.Application.Cars.Commands.Create
{
    /// <summary>
    /// Create car handler
    /// </summary>
    public class CreateCarHandler : IRequestHandler<CreateCar, Car>
    {
        private readonly IUnitOfWork unitOfWorkRepo;
      
        public CreateCarHandler(IUnitOfWork unitOfWorkRepo)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
        }
        /// <summary>
        /// Creates a car
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A new instance of the created car</returns>
        public async Task<Car> Handle(CreateCar request, CancellationToken cancellationToken)
        {
           
            var car = new Car
            {
                Id = request.Id,
                RegNumber = request.RegNumber,
                Make = request.Make,
                Model = request.Model,
                MakeYear = request.MakeYear,
                ImageUrl = request.ImageUrl,
                AirCondition = request.AirCondition,
                Seats = request.Seats,
                Doors = request.Doors,
                NavigationSystem = request.NavigationSystem,
                Fuel = request.Fuel,
                Transmission = request.Transmission,
                CategoryId = request.CategoryId,
                DailyRate = request.DailyRate,
                DealerId = request.DealerId
            };

            await this.unitOfWorkRepo.CarRepository.AddAsync(car);
            await this.unitOfWorkRepo.SaveAsync();

            return car;
        }
    }
}
