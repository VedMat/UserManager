using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace UserManager.Services
{
    public class BlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName = "pitch-deck";

        public BlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AzureBlobStorage");
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> DownloadFileAsync(string fileName)
        {
                var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                await containerClient.CreateIfNotExistsAsync();
                var blobClient = containerClient.GetBlobClient(fileName);
            var sasUri = blobClient.GenerateSasUri(Azure.Storage.Sas.BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddHours(1));
            return sasUri.ToString();
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                await containerClient.CreateIfNotExistsAsync();
                var blobClient = containerClient.GetBlobClient(fileName);
                await blobClient.UploadAsync(fileStream, overwrite: true);
                return blobClient.Uri.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }
        }
    }
}
