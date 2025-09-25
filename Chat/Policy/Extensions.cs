using ChatService.Policy.Handlers;
using ChatService.Policy.Reqierments;
using Microsoft.AspNetCore.Authorization;

namespace ChatService.Policy
{
    public static class Extensions
    {
        public static void AddPolicies(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, UserResourceOwnerHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ResourceOwner", policy =>
                    policy.Requirements.Add(new UserResourceOwnerRequirement()));
            });
        }
    }
}
