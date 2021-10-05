using System;
using System.Threading.Tasks;
using HomeOrganization.DAL;
using HomeOrganization.DAL.Models;
using HomeOrganization.Models.Interfaces;
using HomeOrganization.Services.StorageService;
using Microsoft.EntityFrameworkCore;

namespace HomeOrganization.Services.HomeOrganizationService
{
    public class WebService : IWebService
    {

        private readonly IStorageService _storageService;
        private readonly HomeOrganizationApiDbContext _dbContext;

        public WebService(IStorageService storageService, HomeOrganizationApiDbContext dbContext)
        {
            _storageService = storageService;
            _dbContext = dbContext;
        }
        
        public async Task UploadImage(string userGuid, string imageName, string imageBase64)
        {
            try
            {
                var user = await _dbContext.HomeUsers.FirstOrDefaultAsync(c => c.Id == userGuid);
                
                var homeGroup = await GetHomeGroup(user);
                var fileGuid = Guid.NewGuid();
                await _storageService.UploadFileToDirectory(homeGroup.Name, fileGuid.ToString(), imageBase64);
                var photo = await CreatePhoto(fileGuid.ToString());
                var item = await CreateItem(user, imageName, photo);

                _dbContext.HomeItems.Add(item);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Console.Error.WriteLineAsync(ex.Message);
            }
        }

        private Task<HomePhoto> CreatePhoto(string imageGuid)
        {
            var photo = new HomePhoto();
            photo.BlobHash = imageGuid;
            
            return Task.FromResult(photo);
        }
        
        private async Task<HomeItem> CreateItem(HomeUser user, string imageName, HomePhoto photo)
        {
            var group = await GetHomeGroup(user);
            var item = new HomeItem();
            item.Name = imageName;
            item.HomeGroup = group;
            item.HomeGroupId = group.Id;
            item.HomePhoto = photo;
            return item;
        }
        
        private async Task<HomeGroup> GetHomeGroup(HomeUser user)
        {
            var groupId = (await _dbContext.HomeGroupUsers.FirstOrDefaultAsync(c => c.HomeUserId == user.Id)).HomeGroupId;
            return await _dbContext.HomeGroups.FirstOrDefaultAsync(c => c.Id == groupId);
        }
    }
}