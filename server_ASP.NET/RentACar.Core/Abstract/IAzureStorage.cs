using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Abstract
{
    /// <summary>
    ///  Azure Storage Blob client.
    /// </summary>
    public interface IAzureStorage
    {
        /// <summary>
        /// Adds content to the Azure storage blob.
        /// </summary>
        /// <param name="memoryStream">File.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="contentType">The content type.</param>
        /// <param name="containerName">The container name.</param>
        Task UploadContentAsync(MemoryStream memoryStream, string fileName, string contentType, string containerName);


        /// <summary>
        /// Get default image for person from the Azure storage blob.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="containerName">The container name.</param>
        string GetSingleFile(string fileName, string containerName);
    }
}
