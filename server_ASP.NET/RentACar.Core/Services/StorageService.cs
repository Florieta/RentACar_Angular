using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RentACar.Application.Abstract;
using RentACar.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Services
{
    /// <inheritdoc />
    public class StorageService : IStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Storage"/> class.
        /// </summary>
        public StorageService(
            BlobServiceClient blobServiceClient,
            IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;

        }
        /// <inheritdoc />
        public async Task Upload(FileModel fileModel)
        {
            var containerName = _configuration.GetSection("BlobContainerName").Value;

            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileModel.ImageFile.FileName);

            await blobClient.UploadAsync(fileModel.ImageFile.OpenReadStream());
        }

    }
}
