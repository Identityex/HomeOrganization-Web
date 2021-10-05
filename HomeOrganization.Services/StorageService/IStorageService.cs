using System.Threading.Tasks;

namespace HomeOrganization.Services.StorageService
{
    public interface IStorageService
    {
        /// <summary>
        /// Upload a file to directory using service of choice
        /// </summary>
        /// <param name="directoryName">Name of the directory to be placed (AppData, Blob Container, Redis Table, etc)</param>
        /// <param name="fileGuid">Name of File</param>
        /// <param name="fileBase64">Base64 Content of the file to be uploaded or converted into a file</param>
        /// <returns>Result of file storage attempt</returns>
        public Task<bool> UploadFileToDirectory(string directoryName, string fileGuid, string fileBase64);
    }
}