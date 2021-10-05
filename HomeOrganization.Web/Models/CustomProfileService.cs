using System;
using System.Linq;
using System.Threading.Tasks;
using HomeOrganization.DAL.Models;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace HomeOrganization.Web.Models
{
    public class CustomProfileService : IdentityServer4.AspNetIdentity.ProfileService<HomeUser>
    {
        public CustomProfileService(UserManager<HomeUser> userManager, 
            IUserClaimsPrincipalFactory<HomeUser> claimsFactory) : base(userManager, claimsFactory)
        {
        }

        public CustomProfileService(UserManager<HomeUser> userManager,
            IUserClaimsPrincipalFactory<HomeUser> claimsFactory,
            ILogger<ProfileService<HomeUser>> logger) : base(userManager, claimsFactory, logger)
        {
        }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject?.GetSubjectId();

            if (sub == null)
            {
                throw new Exception("No sub claim present");
            }

            var user = await UserManager.FindByIdAsync(sub);
            if (user == null)
            {
                Logger?.LogWarning("No user found matching subject Id: {0}", sub);
                return;
            }

            var claimsPrincipal = await ClaimsFactory.CreateAsync(user);
            if (claimsPrincipal == null)
            {
                throw new Exception("ClaimsFactory failed to create a principal");
            }
            
            context.RequestedClaimTypes = context.RequestedClaimTypes.Union(new[] { "email" }).ToList();
            context.AddRequestedClaims(claimsPrincipal.Claims);
        }
    }
}