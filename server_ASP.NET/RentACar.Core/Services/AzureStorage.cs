using Amazon.Util.Internal.PlatformServices;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RentACar.Application.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Services
{
    /// <inheritdoc />
    public class AzureStorage : IAzureStorage
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureStorage"/> class.
        /// </summary>
        public AzureStorage(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public async Task UploadContentAsync(MemoryStream memoryStream, string fileName, string contentType, string containerName)
        {
            BlobSasBuilder sasBuilder = new BlobSasBuilder(
                BlobSasPermissions.Read
                | BlobSasPermissions.Write
                | BlobSasPermissions.Create,
                DateTimeOffset.UtcNow.AddHours(1))
            {
                BlobContainerName = containerName,
                BlobName = fileName,
                Resource = "b",
            };

            var blobStorageName = _configuration.GetSection("BlobStorageName").Value;
            var blobStorageKey = _configuration.GetSection("BlobStorageKey").Value;
            var storageSharedKeyCredential = new StorageSharedKeyCredential(blobStorageName, blobStorageKey);

            BlobSasQueryParameters sasQueryParameters = sasBuilder.ToSasQueryParameters(storageSharedKeyCredential);

            UriBuilder fullUri = new UriBuilder
            {
                Scheme = "https",
                Host = $"{blobStorageName}.blob.core.windows.net",
                Path = $"{containerName}/{fileName}",
                Query = sasQueryParameters.ToString(),
            };

            var blobClient = new BlobClient(fullUri.Uri);

            var metadata = new Dictionary<string, string>
            {
                { "DateCreated", DateTime.UtcNow.Ticks.ToString() },
                { "TimeCreated", DateTime.UtcNow.Ticks.ToString() },
            };

            await blobClient.UploadAsync(memoryStream, new BlobHttpHeaders { ContentType = contentType });
            memoryStream.Close();

            await blobClient.SetMetadataAsync(metadata);
        }

      
        /// <inheritdoc />
        public string GetSingleFile(string fileName, string containerName)
        {
            string result = string.Empty;

            var blobConnectionString = _configuration.GetSection("BlobConnectionString").Value;
            var container = new BlobContainerClient(blobConnectionString, containerName);
            BlobClient blobClient = container.GetBlobClient(fileName);

            if (blobClient.CanGenerateSasUri)
            {
                // Create a SAS token that's valid for one day.
                BlobSasBuilder sasBuilder = new BlobSasBuilder(BlobSasPermissions.Read | BlobSasPermissions.Write, DateTimeOffset.UtcNow.AddDays(1))
                {
                    BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
                    BlobName = blobClient.Name,
                    Resource = "b",
                };

                Uri sasUri = blobClient.GenerateSasUri(sasBuilder);

                result = sasUri.AbsoluteUri;
            }

            return result;
        }
    }
}
