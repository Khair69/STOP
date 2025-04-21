using Microsoft.AspNetCore.Authorization;
using STOP.Authorization.Policies;

namespace STOP.Authorization.Handlers
{
    public class IsAdminHandler : AuthorizationHandler<CanDeleteDogRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            CanDeleteDogRequirement requirement)
        {
            if (context.User.HasClaim("IsAdmin", "true"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
