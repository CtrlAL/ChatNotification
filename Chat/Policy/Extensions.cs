using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain;
using ChatService.Domain.Filters;
using ChatService.Policy.Autorizators.Implementations;
using ChatService.Policy.Autorizators.Interfaces;
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
                options.AddPolicy(PolicyNames.ResourceOwner, policy =>
                    policy.Requirements.Add(new UserResourceOwnerRequirement()));
            });

            services.AddAutorizators();
        }

        private static void AddAutorizators(this IServiceCollection services)
        {
            services.AddScoped<IChatAutorizator, ChatAutorizator>();
            services.AddScoped<IChatMessageAuthorizator, ChatMessageAuthorizator>();
        }
    }
}
