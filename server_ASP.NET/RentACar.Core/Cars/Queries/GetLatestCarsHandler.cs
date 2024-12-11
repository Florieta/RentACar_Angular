using MediatR;
using Microsoft.Extensions.Configuration;
using RentACar.Application.Abstract;
using RentACar.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Cars.Queries
{
    /// <summary>
    /// Get latest cars handler
    /// </summary>
    public class GetLatestCarsHandler : IRequestHandler<GetLatestCars, List<Car>>
    {
        private readonly IUnitOfWork unitOfWorkRepo;
        private readonly IAzureStorage azureStorage;
        private readonly IConfiguration configuration;
        public GetLatestCarsHandler(IUnitOfWork unitOfWorkRepo, IAzureStorage azureStorage, IConfiguration configuration)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
            this.azureStorage = azureStorage;
            this.configuration = configuration;
        }
        /// <summary>
        /// Gets latest cars
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns a list of latest cars </returns>
        public async Task<List<Car>> Handle(GetLatestCars request, CancellationToken cancellationToken)
        {
            var result = await this.unitOfWorkRepo.CarRepository.GetLatestCarsAsync();
            var containerName = configuration.GetSection("BlobContainerName").Value;

            foreach (var car in result)
            {
                car.ImageUrl = azureStorage.GetSingleFile(car.ImageUrl, containerName);
            }

            return result;

        }
    }
}
