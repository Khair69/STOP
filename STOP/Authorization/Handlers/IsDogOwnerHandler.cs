using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using STOP.Authorization.Policies;
using STOP.Models;

namespace STOP.Authorization.Handlers
{
    public class IsDogOwnerHandler : AuthorizationHandler<IsDogOwnerRequirement, Dog>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IsDogOwnerHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsDogOwnerRequirement requirement, Dog resource)
        {
            var appUser = await _userManager.GetUserAsync(context.User);
            if (appUser == null)
            {
                return;
            }
            if (resource.OwnerId == appUser.Id)
            {
                context.Succeed(requirement);
            }
        }
    }

    public class IsDogOwnerDelHandler : AuthorizationHandler<CanDeleteDogRequirement, Dog> 
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IsDogOwnerDelHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CanDeleteDogRequirement requirement, Dog resource)
        {
            var appUser = await _userManager.GetUserAsync(context.User);
            if (appUser == null)
            {
                return;
            }
            if (resource.OwnerId == appUser.Id)
            {
                context.Succeed(requirement);
            }
        }
    }
}
