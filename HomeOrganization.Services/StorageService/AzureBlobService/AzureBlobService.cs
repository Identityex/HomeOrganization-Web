using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace HomeOrganization.Services.StorageService.AzureBlobService
{
    public class AzureBlobService : IStorageService
    {
        private CloudStorageAccount _cloudStorageAccount;
        private CloudBlobClient _blobClient;
        
        public AzureBlobService(string connString)
        {
            _cloudStorageAccount = CloudStorageAccount.Parse(connString);
            _blobClient = _cloudStorageAccount.CreateCloudBlobClient();
        }
        
        
        public async Task<bool> UploadFileToDirectory(string directoryName, string fileGuid, string fileBase64)
        {
            try
            {
                var container = _blobClient.GetContainerReference(directoryName);
                if (await container.CreateIfNotExistsAsync())
                {
                    
                }

                var blockBlob = container.GetBlockBlobReference(fileGuid);
                await blockBlob.UploadTextAsync(fileBase64);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}