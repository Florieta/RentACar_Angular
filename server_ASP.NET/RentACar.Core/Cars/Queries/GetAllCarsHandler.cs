using MediatR;
using Microsoft.Extensions.Configuration;
using RentACar.Application.Abstract;
using RentACar.Application.Services;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Cars.Queries
{
    /// <summary>
    /// Get all cars handler
    /// </summary>
    public class GetAllCarsHandler : IRequestHandler<GetAllCars, List<Car>>
    {
        private readonly IUnitOfWork unitOfWorkRepo;
        private readonly IAzureStorage azureStorage;
        private readonly IConfiguration configuration;
        public GetAllCarsHandler(IUnitOfWork unitOfWorkRepo, IAzureStorage azureStorage, IConfiguration configuration)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
            this.azureStorage = azureStorage;
            this.configuration = configuration;
        }
        /// <summary>
        /// Gets all cars
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns a list of cars </returns>
        public async Task<List<Car>> Handle(GetAllCars request, CancellationToken cancellationToken)
        {
            var result = await this.unitOfWorkRepo.CarRepository.GetAllAsync();
            var containerName = configuration.GetSection("BlobContainerName").Value;
            
            foreach (var car in result)
            {
                car.ImageUrl = azureStorage.GetSingleFile(car.ImageUrl, containerName);
            }

            return result;

        }
    }
}
