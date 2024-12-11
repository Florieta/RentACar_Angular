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
    /// Get car by id handler
    /// </summary>
    public class GetCarByIdHandler : IRequestHandler<GetCarById, Car>
    {
        private readonly IUnitOfWork unitOfWorkRepo;
        private readonly IAzureStorage azureStorage;
        private readonly IConfiguration configuration;
        public GetCarByIdHandler(IUnitOfWork unitOfWorkRepo,IAzureStorage azureStorage, IConfiguration configuration)
        {
            this.unitOfWorkRepo = unitOfWorkRepo;
            this.azureStorage = azureStorage;
            this.configuration = configuration;
        }
        /// <summary>
        /// Get a car by identifier
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns a car</returns>
        public async Task<Car> Handle(GetCarById request, CancellationToken cancellationToken)
        {
            var result =  await this.unitOfWorkRepo.CarRepository.GetByIdAsync(request.Id);
            var containerName = configuration.GetSection("BlobContainerName").Value;
            result.ImageUrl = azureStorage.GetSingleFile(result.ImageUrl, containerName);

            return result;
        }
    }
}
