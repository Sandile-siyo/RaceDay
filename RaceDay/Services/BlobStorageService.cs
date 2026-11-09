using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace RaceDay.Services
{
    public class BlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        private const string ContainerName = "event-images";

        public BlobStorageService(IConfiguration configuration)
        {
            string connectionString =
                configuration.GetConnectionString("AzureBlobStorage")
                ?? throw new InvalidOperationException(
                    "Azure Blob Storage connection string is missing.");

            _blobServiceClient =
                new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadImageAsync(
            Stream fileStream,
            string fileName,
            string contentType)
        {
            // Get the event-images container.
            BlobContainerClient containerClient =
                _blobServiceClient.GetBlobContainerClient(ContainerName);

            // Create the container as PRIVATE if it does not already exist.
            await containerClient.CreateIfNotExistsAsync(
                PublicAccessType.None);

            // Get the file extension.
            string extension =
                Path.GetExtension(fileName);

            // Generate a unique name for the image.
            string blobName =
                $"{Guid.NewGuid()}{extension}";

            // Get a reference to the new blob.
            BlobClient blobClient =
                containerClient.GetBlobClient(blobName);

            // Keep the image's correct content type.
            BlobHttpHeaders headers =
                new BlobHttpHeaders
                {
                    ContentType = contentType
                };

            BlobUploadOptions uploadOptions =
                new BlobUploadOptions
                {
                    HttpHeaders = headers
                };

            // Upload the image to Azure Blob Storage.
            await blobClient.UploadAsync(
                fileStream,
                uploadOptions);

            // Return the Azure Blob URL.
            return blobClient.Uri.ToString();
        }
    }
}