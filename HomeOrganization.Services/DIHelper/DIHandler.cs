using Autofac;
using Autofac.Core;
using HomeOrganization.DAL;
using HomeOrganization.Models.Interfaces;
using HomeOrganization.Services.HomeOrganizationService;
using HomeOrganization.Services.StorageService;
using HomeOrganization.Services.StorageService.AzureBlobService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HomeOrganization.Services.DIHelper
{
    public class DIHandler : Module
    {
        private IConfiguration _config;
    
        public DIHandler() { }
    
        public DIHandler(IConfiguration config)
        {
            _config = config;
        }
    
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AzureBlobService>().As<IStorageService>()
                .WithParameter("connString", _config.GetConnectionString("BlobKey"));
            builder.RegisterType<WebService>().As<IWebService>();

            var options = new DbContextOptions<HomeOrganizationApiDbContext>();
            builder.RegisterInstance(options);
        }

    }
}