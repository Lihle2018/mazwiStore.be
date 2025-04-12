using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using mazwiStore.be.Services.Interfaces;

namespace mazwiStore.be.Services
{
    public class StorageService(BlobServiceClient _blobServiceClient, string _storageContainerName) :IStorageService
    {
        public async Task<string> SaveAsync(IFormFile file)
        {
            var container = _blobServiceClient.GetBlobContainerClient(_storageContainerName);
            await container.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var blobClient = container.GetBlobClient(Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });

            return blobClient.Uri.ToString();
        }
    }
}
