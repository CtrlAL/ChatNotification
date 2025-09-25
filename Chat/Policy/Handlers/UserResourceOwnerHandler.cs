using ChatService.Domain.Interfaces;
using ChatService.Policy.Reqierments;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ChatService.Policy.Handlers
{
    public class UserResourceOwnerHandler : AuthorizationHandler<UserResourceOwnerRequirement, IUserResourse>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            UserResourceOwnerRequirement requirement,
            IUserResourse resource)
        {
            var currentUserId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId == resource.UserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
