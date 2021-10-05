using System.Threading.Tasks;
using HomeOrganization.DAL.Models;

namespace HomeOrganization.Models.Interfaces
{
    public interface IWebService
    {
        public Task UploadImage(string userGuid, string imageName, string imageBase64);
    }
}