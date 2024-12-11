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
    /// Get all car by dealerId handler
    /// </summary>
    public class GetAllCarsByDealerIdHandler : IRequestHandler<GetAllCarsByDealerId, List<Car>>
    {
        private readonly IUnitOfWork unitOfWorkRepo;
        private readonly IAzureStorage azureStorage;
        private readonly IConfiguration configuration;
        public GetAllCarsByDealerIdHandler(IUnitOfWork unitOfWorkRepo, IAzureStorage azureStorage, IConfiguration configuration)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
            this.azureStorage = azureStorage;
            this.configuration = configuration;
        }
        /// <summary>
        /// Gets all cars by identifier
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns a list of cars by delaerId</returns>
        public async Task<List<Car>> Handle(GetAllCarsByDealerId request, CancellationToken cancellationToken)
        {
            var result = await this.unitOfWorkRepo.CarRepository.GetCarsByDealerIdAsync(request.DealerId);
            var containerName = configuration.GetSection("BlobContainerName").Value;

            foreach (var car in result)
            {
                car.ImageUrl = azureStorage.GetSingleFile(car.ImageUrl, containerName);
            }

            return result;
        }
    }
}
