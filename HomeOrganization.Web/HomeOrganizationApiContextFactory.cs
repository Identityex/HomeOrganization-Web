using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HomeOrganization.DAL
{
    public class HomeOrganizationApiContextFactory : IDesignTimeDbContextFactory<HomeOrganizationApiDbContext>
    {
        public HomeOrganizationApiDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HomeOrganizationApiDbContext>();
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));
            
            return new HomeOrganizationApiDbContext(optionsBuilder.Options, new OptionsWrapper<OperationalStoreOptions>(new OperationalStoreOptions()));
        }
    }
}